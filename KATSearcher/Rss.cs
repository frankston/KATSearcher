using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace KATSearcher
{
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute("rss", Namespace = "", IsNullable = false)]
    public class Rss
    {
        [XmlElementAttribute("channel", Form = XmlSchemaForm.Unqualified)]
        public RssChannel[] Channel { get; set; }

        [XmlAttributeAttribute("version")]
        public string Version { get; set; }

        [XmlIgnore]
        public Uri RequestUri { get; set; }

        public override string ToString()
        {
            return RequestUri.ToString();
        }

        public static async Task<Rss> GetPage(Uri requestUri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(requestUri);
                if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new NoResultsReturnedException();

                var responseXml = await response.Content.ReadAsStringAsync();
                var xmlSerializer = new XmlSerializer(typeof(Rss));
                using (var reader = new StringReader(responseXml))
                {
                    var result = (Rss)xmlSerializer.Deserialize(reader);
                    result.RequestUri = requestUri;
                    return result;
                }
            }
        }

        public static async Task<List<Rss>> GetPages(ISearchParams searchParams, int maximumPages)
        {
            var results = new List<Rss>();
            int currentPage = 1;
            while (maximumPages >= currentPage)
            {
                var rssUri = BuildUri(searchParams, currentPage);
                var rssPage = await Rss.GetPage(rssUri);

                if (rssPage == null || rssPage.Channel[0].Items.Length == 0)
                    return results;

                Debug.WriteLine("{0} (page {1}: {2} items)", rssUri, currentPage, rssPage.Channel[0].Items.Length);

                // Determine if last page has been reached - KAT returns first results page for any requested page numbers that don't exist
                if (rssPage != null && rssPage.Channel.Length > 0 && results.Count > 0 && rssPage.Channel[0].Items[0].InfoHash == results[0].Channel[0].Items[0].InfoHash)
                    return results;

                results.Insert(results.Count, rssPage);

                currentPage++;
            }
            return results;
        }

        public static Uri BuildUri(ISearchParams searchParameters)
        {
            return BuildUri(searchParameters, 1, null, null);
        }

        public static Uri BuildUri(ISearchParams searchParameters, int pageNumber)
        {
            return BuildUri(searchParameters, pageNumber, null, null);
        }

        public static Uri BuildUri(ISearchParams searchParameters, int pageNumber, string katDomain, bool? useHttps)
        {
            // TODO: This method could do with some refactoring
            var wholeUri = new StringBuilder();
            wholeUri.Append(useHttps ?? true ? "https://" : "http://");
            wholeUri.Append(string.IsNullOrWhiteSpace(katDomain) ? "kickass.to" : katDomain);
            wholeUri.Append("/usearch/");

            if (searchParameters != null)
            {
                var query = new StringBuilder();

                // AllTheseWords
                AppendQuery(ref query, searchParameters.AllTheseWords, " ", null);

                // ThisExactWordingOrPhrase
                AppendQuery(ref query, searchParameters.ThisExactWordingOrPhrase, "\"");

                // AnyOfTheseWords
                AppendQuery(ref query, searchParameters.AnyOfTheseWords, " OR ", null);

                // SubtractSpecifiedWords
                if (searchParameters.SubtractSpecifiedWords != null)
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Join(" -", searchParameters.SubtractSpecifiedWords.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => WebUtility.HtmlEncode(x))), "-"));

                // Category
                if (searchParameters.Category != null && searchParameters.Category != Category.any)
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Format("category:{0}", StandardiseEnumName(searchParameters.Category))));

                // UploadsByCertainUser
                if (!string.IsNullOrWhiteSpace(searchParameters.UploadsByCertainUser))
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Format("user:{0}", searchParameters.UploadsByCertainUser)));

                // MinimumSeeders
                AppendQuery(ref query, "seeds", searchParameters.MinimumSeeders);

                //AddedAge
                if (searchParameters.AddedAge != null && searchParameters.AddedAge != AddedAge.any)
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Format("age:{0}", searchParameters.AddedAge.ToString().Replace("_", ""))));

                // NumberOfFiles
                AppendQuery(ref query, "files", searchParameters.NumberOfFiles);

                // ImdbId
                AppendQuery(ref query, "imdb", searchParameters.ImdbId);

                // MovieOrTvShowLanguage
                if (searchParameters.MovieOrTvShowLanguage != null && searchParameters.MovieOrTvShowLanguage != KATSearcher.MovieOrTvShowLanguage.any)
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Format("lang_id:{0}", (int)searchParameters.MovieOrTvShowLanguage)));

                // FamilySafetyFilter
                AppendQuery(ref query, "is_safe", searchParameters.FamilySafetyFilter);

                // OnlyVerifiedTorrents
                AppendQuery(ref query, "verified", searchParameters.OnlyVerifiedTorrents);

                // TvShowSeason
                AppendQuery(ref query, "season", searchParameters.TvShowSeason);

                // TvShowEpisode
                AppendQuery(ref query, "episode", searchParameters.TvShowEpisode);

                // GamePlatform
                if (searchParameters.GamePlatform != null && searchParameters.GamePlatform != GamePlatform.any)
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Format("platform_id:{0}", (int)searchParameters.GamePlatform)));

                if (!string.IsNullOrWhiteSpace(query.ToString()))
                    wholeUri.AppendFormat("{0}/", query.ToString());
            }

            wholeUri.Append(string.Format("{0}/?rss=1", pageNumber));
            return new Uri(wholeUri.ToString());
        }


        private static string AddOptionalPreSpace(string existingQuery, string queryPartToAdd)
        {
            return AddOptionalPreSpace(existingQuery, queryPartToAdd, null);
        }

        private static string AddOptionalPreSpace(string existingQuery, string queryPartToAdd, string queryPrefix)
        {
            return !string.IsNullOrWhiteSpace(existingQuery) && !string.IsNullOrWhiteSpace(queryPartToAdd) ? string.Format(" {0}{1}", string.IsNullOrWhiteSpace(queryPrefix) ? "" : queryPrefix, queryPartToAdd) : queryPartToAdd;
        }

        private static void AppendQuery(ref StringBuilder query, string key, bool? value)
        {
            if (value != null)
                query.Append(AddOptionalPreSpace(query.ToString(), string.Format("{0}:{1}", key, (bool)value ? 1 : 0)));
        }

        private static void AppendQuery(ref StringBuilder query, string key, int? value)
        {
            if (value != null)
                query.Append(AddOptionalPreSpace(query.ToString(), string.Format("{0}:{1}", key, value.ToString())));
        }

        private static void AppendQuery(ref StringBuilder query, IEnumerable<string> value, string separator, string surroundWith)
        {
            if (value != null)
            {
                var joined = string.Join(separator, value.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => WebUtility.HtmlEncode(x)));

                if (!string.IsNullOrWhiteSpace(surroundWith))
                    joined = string.Format("{0}{1}{0}", surroundWith, joined);

                query.Append(AddOptionalPreSpace(query.ToString(), joined));
            }
        }

        private static void AppendQuery(ref StringBuilder query, string value, string surroundWith)
        {
            if (value != null)
            {
                if (!string.IsNullOrWhiteSpace(surroundWith))
                    query.Append(AddOptionalPreSpace(query.ToString(), string.Format("{0}{1}{0}", surroundWith, WebUtility.HtmlEncode(value))));
                else
                    query.Append(AddOptionalPreSpace(query.ToString(), value));
            }
        }

        public static string StandardiseEnumName(Enum value)
        {
            // Replace underscore with nothing (used to allow enum names that begin with a number)
            var tempName = value.ToString().Replace("_", "");

            // Replace uppercase chars with dash followed by lower case char
            var sb = new StringBuilder();
            for (int i = 0; i < tempName.Length; i++)
            {
                if (char.IsUpper(tempName[i]))
                    sb.AppendFormat("-{0}", char.ToLower(tempName[i]));
                else
                    sb.Append(tempName[i]);
            }
            return sb.ToString();
        }
    }
}