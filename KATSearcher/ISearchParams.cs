using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KATSearcher
{
    public interface ISearchParams
    {
        IEnumerable<string> AllTheseWords { get; set; }
        string ThisExactWordingOrPhrase { get; set; }
        IEnumerable<string> AnyOfTheseWords { get; set; }
        IEnumerable<string> SubtractSpecifiedWords { get; set; }
        Category? Category { get; set; }
        string UploadsByCertainUser { get; set; }
        int? MinimumSeeders { get; set; }
        AddedAge? AddedAge { get; set; }
        int? NumberOfFiles { get; set; }
        int? ImdbId { get; set; }
        MovieOrTvShowLanguage? MovieOrTvShowLanguage { get; set; }
        bool? FamilySafetyFilter { get; set; }
        bool? OnlyVerifiedTorrents { get; set; }
        int? TvShowSeason { get; set; }
        int? TvShowEpisode { get; set; }
        GamePlatform? GamePlatform { get; set; }
    }
}