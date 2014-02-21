using System;
using System.Collections.Generic;
using KATSearcher;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class RssTests
    {
        // Notes:
        // * Results from specific GamePlatform search is dodgy - inconsistent results returned. Try it manually on the website...
        // * Likewise, results from Categories are not reliable, hence SearchParams_AllCategoriesTest is a little bit 'rough'
        // * NumberOfFiles - not returned in response therefore no way to test (unless torrent file is downloaded and parsed - possible future test)

        // TODO: create arbitrary mashup of options and check results are valid
        // TODO: create Uri queries via SearchQueryBuilder and compare against expected Uri
        // TODO: check live site to compare 'select' options to enum values - identify if any have been added or removed
        // TODO: pass invalid chars (that must be encoded)


        [TestMethod]
        public void SearchParams_Invalid_AllTheseWordsTest()
        {
            var searchParams = new SearchParams() { AllTheseWords = new List<string>() { Methods.RandomCharString(50) } };
            try
            {
                var pages = Rss.GetPages(searchParams, 1).Result;
            }
            catch (AggregateException ae)
            {
                Assert.IsTrue(ae.InnerException.GetType() == typeof(NoResultsReturnedException));
            }
        }

        [TestMethod]
        public void SearchParams_NoSearchCriteria()
        {
            var pages = Rss.GetPages(null, 5).Result;
            for (int i = 0; i < pages.Count; i++)
            {
                Assert.IsTrue(pages[i].Channel[0].Items.All(x => !Methods.AllPropertiesAreDefaultValues<RssChannelItem>(x)));
            }
        }

        [TestMethod]
        public void SearchParams_AllTheseWordsTest()
        {
            // TODO: Pick randomly from list of known words
            var searchParams = new SearchParams() { AllTheseWords = new List<string>() { "pbs", "nature" } };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Title.Contains("pbs", StringComparison.OrdinalIgnoreCase) && x.Title.Contains("nature", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void SearchParams_ThisExactWordingOrPhraseTest()
        {
            // An exact phrase may include various different 'space' characters (for example, a fullstop) hence the regular expression check
            // TODO: Pick randomly from list of known phrases
            var searchParams = new SearchParams() { ThisExactWordingOrPhrase = "bbc natural world" };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => Regex.IsMatch(x.Title, "bbc[^A-Z]{1}natural[^A-Z]{1}world", RegexOptions.IgnoreCase)));
        }

        [TestMethod]
        public void SearchParams_AnyOfTheseWordsTest()
        {
            var searchParams = new SearchParams() { AnyOfTheseWords = new List<string>() { "pbs", "nature" } };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Title.Contains("pbs", StringComparison.OrdinalIgnoreCase) || x.Title.Contains("nature", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void SearchParams_SubtractSpecifiedWordsTest()
        {
            var searchParams = new SearchParams() { AnyOfTheseWords = new List<string>() { "pbs" }, SubtractSpecifiedWords = new List<string>() { "nature" } };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Title.Contains("pbs", StringComparison.OrdinalIgnoreCase) && !x.Title.Contains("nature", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void SearchParams_AllCategoriesTest()
        {
            var categories = Methods.GetValues<Category>();
            foreach (var category in categories)
            {
                if (category != Category.any && category != Category.unsorted) // Unsorted category is unreliable
                {
                    var comparativeCategoryName = Rss.StandardiseEnumName(category).ToString().Replace("other-", "").Replace("-software", "").Replace("-applications", "").Replace("-games", "").Replace("-movies", "").Replace("-videos", "").Replace("-video", "").Replace("-", " ");

                    var catNames = comparativeCategoryName.Split(new char[] { ' ' });

                    var searchParams = new SearchParams() { Category = category };
                    var pages = Rss.GetPages(searchParams, 1).Result;
                    Assert.IsTrue(pages[0].Channel[0].Items.All(x => catNames.All(y => x.Category.Contains(y, StringComparison.OrdinalIgnoreCase)) || x.Title.Contains(comparativeCategoryName, StringComparison.OrdinalIgnoreCase)));
                }
            }
        }

        [TestMethod]
        public void SearchParams_UploadsByCertainUserTest()
        {
            var searchParams = new SearchParams() { UploadsByCertainUser = "MVGroup" };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Author.Contains("MVGroup", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void SearchParams_MinimumSeedersTest()
        {
            var searchParams = new SearchParams() { MinimumSeeders = 10 };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => int.Parse(x.Seeds) >= 10));
        }

        // TODO: Added age

        // TODO: IMDB ID

        // TODO: MovieOrTvShowLanguage

        // TODO: FamilySafetyFilter


        [TestMethod]
        public void SearchParams_OnlyVerifiedTorrentsTest()
        {
            var searchParams = new SearchParams() { OnlyVerifiedTorrents = true };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Verified == "1"));
        }


        [TestMethod]
        public void SearchParams_TvShowSeasonTest()
        {
            // TODO: Make stronger check
            var searchParams = new SearchParams() { TvShowSeason = 21, ThisExactWordingOrPhrase = "Simpsons" };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Title.Contains("21", StringComparison.OrdinalIgnoreCase)));
        }


        [TestMethod]
        public void SearchParams_TvShowEpisodeTest()
        {
            // TODO: Make stronger check
            var searchParams = new SearchParams() { TvShowEpisode = 19, ThisExactWordingOrPhrase = "Simpsons" };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Title.Contains("19", StringComparison.OrdinalIgnoreCase)));
        }

        [TestMethod]
        public void SearchParams_GamePlatformTest()
        {
            // TODO: iterate automatically through all game platforms
            var searchParams = new SearchParams() { GamePlatform = GamePlatform.andriod };
            var pages = Rss.GetPages(searchParams, 1).Result;
            Assert.IsTrue(pages[0].Channel[0].Items.All(x => x.Category.Contains("games", StringComparison.OrdinalIgnoreCase)));
        }

    }
}