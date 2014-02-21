using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KATSearcher
{

    public enum Category
    {
        /// <summary>
        /// Any Category
        /// </summary>
        any = 0,

        /// <summary>
        /// All Anime Category
        /// </summary>
        anime,

        /// <summary>
        /// Other Anime
        /// </summary>
        otherAnime,

        /// <summary>
        /// All Applications Category
        /// </summary>
        applications,

        /// <summary>
        /// Windows
        /// </summary>
        windows,

        /// <summary>
        /// Mac
        /// </summary>
        macSoftware,

        /// <summary>
        /// UNIX
        /// </summary>
        unix,

        /// <summary>
        /// iOS
        /// </summary>
        ios,

        /// <summary>
        /// Andriod
        /// </summary>
        android,

        /// <summary>
        /// Handheld Applications
        /// </summary>
        handheldApplications,

        /// <summary>
        /// Other Applications
        /// </summary>
        otherApplications,

        /// <summary>
        /// All Books Category
        /// </summary>
        books,

        /// <summary>
        /// EBooks
        /// </summary>
        ebooks,

        /// <summary>
        /// Comics
        /// </summary>
        comics,

        /// <summary>
        /// Magazines
        /// </summary>
        magazines,

        /// <summary>
        /// Textbooks
        /// </summary>
        textbooks,

        /// <summary>
        /// Fiction
        /// </summary>
        fiction,

        /// <summary>
        /// Non-Fiction
        /// </summary>
        nonFiction,

        /// <summary>
        /// Audio Books
        /// </summary>
        audioBooks,

        /// <summary>
        /// Academic Books
        /// </summary>
        academic,

        /// <summary>
        /// Other Books
        /// </summary>
        otherBooks,

        /// <summary>
        /// All Games Category
        /// </summary>
        games,

        /// <summary>
        /// PC Games
        /// </summary>
        pcGames,

        /// <summary>
        /// Mac Games
        /// </summary>
        macGames,

        /// <summary>
        /// PS2 Games
        /// </summary>
        ps2,

        /// <summary>
        /// XBOX360 Games
        /// </summary>
        xbox360,

        /// <summary>
        /// Wii Games
        /// </summary>
        wii,

        /// <summary>
        /// Handheld Games
        /// </summary>
        handheldGames,

        /// <summary>
        /// NDS Games
        /// </summary>
        nds,

        /// <summary>
        /// PSP Games
        /// </summary>
        psp,

        /// <summary>
        /// PS3 Games
        /// </summary>
        ps3,

        /// <summary>
        /// iOS Games
        /// </summary>
        iosGames,

        /// <summary>
        /// Andriod Games
        /// </summary>
        androidGames,

        /// <summary>
        /// Other Games
        /// </summary>
        otherGames,

        /// <summary>
        /// All Movies Category
        /// </summary>
        movies,

        /// <summary>
        /// 3D Movies
        /// </summary>
        _3dMovies,

        /// <summary>
        /// Music Videos
        /// </summary>
        musicVideos,

        /// <summary>
        /// Movie Clips
        /// </summary>
        movieClips,

        /// <summary>
        /// Hendheld Movies
        /// </summary>
        handheldMovies,

        /// <summary>
        /// iPad Movies
        /// </summary>
        ipadMovies,

        /// <summary>
        /// Highres Movies
        /// </summary>
        highresMovies,

        /// <summary>
        /// Bollywood Movies
        /// </summary>
        bollywood,

        /// <summary>
        /// Concert Movies
        /// </summary>
        concerts,

        /// <summary>
        /// Dubbed Movies
        /// </summary>
        dubbedMovies,

        /// <summary>
        /// Asian Movies
        /// </summary>
        asian,

        /// <summary>
        /// Documentaries
        /// </summary>
        documentary,

        /// <summary>
        /// Movie Trailers
        /// </summary>
        trailer,

        /// <summary>
        /// Other Movies
        /// </summary>
        otherMovies,

        /// <summary>
        /// All Music Category
        /// </summary>
        music,

        /// <summary>
        /// Mp3 Music
        /// </summary>
        mp3,

        /// <summary>
        /// AAC Music
        /// </summary>
        aac,

        /// <summary>
        /// Lossless Music
        /// </summary>
        lossless,

        /// <summary>
        /// Transcode Music
        /// </summary>
        transcode,

        /// <summary>
        /// Other Music
        /// </summary>
        otherMusic,

        /// <summary>
        /// All Other Category
        /// </summary>
        other,

        /// <summary>
        /// Pictures
        /// </summary>
        pictures,

        /// <summary>
        /// Sound Clips
        /// </summary>
        soundClips,

        /// <summary>
        /// Covers
        /// </summary>
        covers,

        /// <summary>
        /// Wallpapers
        /// </summary>
        wallpapers,

        /// <summary>
        /// Tutorials
        /// </summary>
        tutorials,

        /// <summary>
        /// Unsorted
        /// </summary>
        unsorted,

        /// <summary>
        /// All TV Category
        /// </summary>
        tv,

        /// <summary>
        /// Other TV
        /// </summary>
        otherTv,

        /// <summary>
        /// All XXX Category
        /// </summary>
        xxx,

        /// <summary>
        /// XXX Video
        /// </summary>
        xxxVideo,

        /// <summary>
        /// XXX HD Video
        /// </summary>
        xxxHdVideo,

        /// <summary>
        /// XXX Pictures
        /// </summary>
        xxxPictures,

        /// <summary>
        /// XXX Magazines
        /// </summary>
        xxxMagazines,

        /// <summary>
        /// XXX Books
        /// </summary>
        xxxBooks,

        /// <summary>
        /// Hentai XXX
        /// </summary>
        hentai,

        /// <summary>
        /// Other XXX
        /// </summary>
        otherXxx
    }

    public enum AddedAge
    {
        /// <summary>
        /// Anytime
        /// </summary>
        any = 0,

        /// <summary>
        /// Past Hour
        /// </summary>
        hour,

        /// <summary>
        /// Past 24 Hours
        /// </summary>
        _24h,

        /// <summary>
        /// Past Week
        /// </summary>
        week,

        /// <summary>
        /// Past Month
        /// </summary>
        month,

        /// <summary>
        /// Past Year
        /// </summary>
        year
    }

    public enum MovieOrTvShowLanguage
    {
        any = 0,
        english = 2,
        albanian = 42,
        arabic = 7,
        basque = 44,
        brazilian = 39,
        bulgarian = 37,
        chinese = 10,
        croatian = 34,
        czech = 32,
        danish = 26,
        dutch = 8,
        filipino = 11,
        finnish = 31,
        french = 5,
        german = 4,
        greek = 30,
        hebrew = 25,
        hindi = 6,
        hungarian = 27,
        italian = 3,
        japanese = 15,
        korean = 16,
        lithuanian = 43,
        malayalam = 21,
        mandarin = 23,
        norwegian = 19,
        persian = 33,
        polish = 9,
        portuguese = 17,
        punjabi = 35,
        romanian = 18,
        russian = 12,
        serbian = 28,
        slovenian = 36,
        spanish_latin_america = 41,
        spanish_spain = 14,
        swedish = 20,
        tamil = 13,
        telugu = 22,
        thai = 24,
        turkish = 29,
        ukrainian = 40,
        vietnamese = 38
    }

    public enum GamePlatform
    {
        any = 0,
        andriod = 4,
        blackBerry = 7,
        gameCube = 15,
        iPad = 18,
        iPhone = 19,
        iPod = 20,
        java = 22,
        linux = 24,
        mac = 25,
        nintendo3DS = 31,
        nintendoDS = 33,
        nuon_dvd = 35,
        other = 65,
        palmOs = 37,
        pc = 38,
        playStation2 = 43,
        playStation3 = 44,
        psp = 45,
        symbian = 52,
        wii = 56,
        windowsCE = 57,
        windowsMobile = 58,
        windowsPhone = 59,
        xbox = 61,
        xbox360 = 62
    }

}