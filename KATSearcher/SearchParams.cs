using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KATSearcher
{
    /// <summary>
    /// This class is provided purely for convenience.
    /// Use this class if you do not want to implement your own class to implement ISearchParams.
    /// </summary>
    public class SearchParams : ISearchParams
    {
        public IEnumerable<string> AllTheseWords { get; set; }
        public string ThisExactWordingOrPhrase { get; set; }
        public IEnumerable<string> AnyOfTheseWords { get; set; }
        public IEnumerable<string> SubtractSpecifiedWords { get; set; }
        public Category? Category { get; set; }
        public string UploadsByCertainUser { get; set; }
        public int? MinimumSeeders { get; set; }
        public AddedAge? AddedAge { get; set; }
        public int? NumberOfFiles { get; set; }
        public int? ImdbId { get; set; }
        public MovieOrTvShowLanguage? MovieOrTvShowLanguage { get; set; }
        public bool? FamilySafetyFilter { get; set; }
        public bool? OnlyVerifiedTorrents { get; set; }
        public int? TvShowSeason { get; set; }
        public int? TvShowEpisode { get; set; }
        public GamePlatform? GamePlatform { get; set; }
    }
}