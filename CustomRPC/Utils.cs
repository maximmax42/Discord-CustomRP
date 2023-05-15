using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CustomRPC
{
    /// <summary>
    /// Native Windows functions.
    /// </summary>
    /// <remarks>
    /// Something adapted from <see href="https://www.codeproject.com/Articles/32908/C-Single-Instance-App-With-the-Ability-To-Restore"/>, something from ShareX.
    /// </remarks>
    public static class WinApi
    {
        [DllImport("user32")]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        private const int DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1 = 19;
        private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;

        public static bool UseImmersiveDarkMode(IntPtr handle)
        {
            if (IsWindows10OrGreater(17763))
            {
                var attribute = DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attribute = DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int useImmersiveDarkMode = Properties.Settings.Default.darkMode ? 1 : 0;
                return DwmSetWindowAttribute(handle, attribute, ref useImmersiveDarkMode, sizeof(int)) == 0;
            }

            return false;
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            return Environment.OSVersion.Version.Major >= 10 && Environment.OSVersion.Version.Build >= build;
        }
    }

    /// <summary>
    /// A struct describing a person.
    /// </summary>
    struct Person
    {
        /// <summary>
        /// Nickname of the person.
        /// </summary>
        public string Name;
        /// <summary>
        /// URL of the person, optional.
        /// </summary>
        public string Url;
    }

    /// <summary>
    /// A struct describing a language.
    /// </summary>
    struct Language
    {
        /// <summary>
        /// Native name of the language.
        /// </summary>
        public string Name;
        /// <summary>
        /// English name of the language.
        /// </summary>
        public string EnglishName;
        /// <summary>
        /// ISO code of the language.
        /// </summary>
        public string Code;
        /// <summary>
        /// A country the language is used in, optional.
        /// </summary>
        public string Dialect;
        /// <summary>
        /// An array of translators.
        /// </summary>
        public Person[] Translators;
    }

    /// <summary>
    /// A struct describing a supporter.
    /// </summary>
    struct Supporter
    {
        /// <summary>
        /// Nickname of the supporter.
        /// </summary>
        public string Name;
        /// <summary>
        /// URL of the supporter, optional.
        /// </summary>
        public string Url;
        /// <summary>
        /// The amount of money the person donated in dollars.
        /// </summary>
        public string USDAmount;
        /// <summary>
        /// The amount of money the person donated in currency other than dollars, optional.
        /// </summary>
        public string AltAmount;

        /// <summary>
        /// Creates a supporter from an existing person.
        /// </summary>
        /// <param name="person">An existing Person object.</param>
        /// <param name="USDAmount">Same as <see cref="Supporter.USDAmount"/>.</param>
        /// <param name="AltAmount">Same as <see cref="Supporter.AltAmount"/>.</param>
        public Supporter(Person person, string USDAmount = "", string AltAmount = "")
        {
            Name = person.Name;
            Url = person.Url;
            this.USDAmount = USDAmount;
            this.AltAmount = AltAmount;
        }
    }

    /// <summary>
    /// A struct describing a non-monetary supporter.
    /// </summary>
    struct NonMonetarySupporter
    {
        /// <summary>
        /// Nickname of the supporter.
        /// </summary>
        public string Name;
        /// <summary>
        /// URL of the supporter, optional.
        /// </summary>
        public string Url;
        /// <summary>
        /// What the supporter donated.
        /// </summary>
        public string DonationType;
        /// <summary>
        /// Link to the donation piece, optional.
        /// </summary>
        public string DonationUrl;

        /// <summary>
        /// Creates a non-monetary supporter from an existing person.
        /// </summary>
        /// <param name="person">An existing Person object.</param>
        /// <param name="DonationType">Same as <see cref="NonMonetarySupporter.DonationType"/>.</param>
        /// <param name="DonationUrl">Same as <see cref="NonMonetarySupporter.DonationUrl"/>.</param>
        public NonMonetarySupporter(Person person, string DonationType = "", string DonationUrl = "")
        {
            Name = person.Name;
            Url = person.Url;
            this.DonationType = DonationType;
            this.DonationUrl = DonationUrl;
        }
    }

    /// <summary>
    /// Utility functions.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// List of languages and their translators used to populate Settings -> Languages and Help -> Translators menu items.<br/>
        /// It is done this way because visual designer doesn't work properly with a lot of items.
        /// </summary>
        static List<Language> Languages { get; set; }

        /// <summary>
        /// Progress for each language.
        /// Not part of the <see cref="Language"/> struct because the progress is generated for me by a separate piece of code. 
        /// </summary>
        static Dictionary<string, string> LanguageProgress { get; set; }

        /// <summary>
        /// List of supporters used to populate Help -> supporters menu item.
        /// </summary>
        static List<Supporter> Supporters { get; set; }

        /// <summary>
        /// List of supporters who have donated something that doesn't have set monetary value (furry art, mostly) used to populate Help -> supporters menu item.
        /// </summary>
        static List<NonMonetarySupporter> NonMonetarySupporters { get; set; }

        // List of people who appear in the lists more than one time
        static Person DarlingChan = new Person { Name = "Darling-Chan", Url = "https://meap.gg/" };
        static Person dragonGRaf = new Person { Name = "dragon GRaf", Url = "https://github.com/dragonGRaf1312/mycustomrichpresence" };
        static Person falcon = new Person { Name = "falcon" };
        static Person GreenRosie = new Person { Name = "GreenRosie", Url = "https://www.twitch.tv/greenrosie" };
        static Person iRetrozx = new Person { Name = "iRetrozx#7283", Url = "https://posted.adalo.com/iretrozx" };
        static Person Julian = new Person { Name = "Julian", Url = "https://julian-idl.codes/" };
        static Person MarcelGustin = new Person { Name = "Marcel Gustin", Url = "https://marcelgustin.de" };
        static Person Matthww = new Person { Name = "Matthww" };
        static Person Meelis = new Person { Name = "Meelis" };
        static Person Mykm = new Person { Name = "Mykm", Url = "https://github.com/yumiruuwu" };
        static Person TofixRs = new Person { Name = "Tofix.rs" };
        static Person Yoshi = new Person { Name = "Yoshi" };
        static Person westxlu = new Person { Name = "蘆筍 (aka westxlu)", Url = "https://linktr.ee/westxlu" };

        static Utils()
        {
            Languages = new List<Language> {
                new Language {
                    Name = "مَصرى",
                    EnglishName = "Arabic",
                    Code = "ar",
                    Translators = new Person[] {
                        new Person { Name = "FiberAhmed", Url = "https://github.com/FiberAhmed" },
                        new Person { Name = "ShadowlGamer" },
                        new Person { Name = "karimawi" },
                        new Person { Name = "Bo Raghad" },
                    }
                },
                new Language {
                    Name = "Беларуская",
                    EnglishName = "Belarusian",
                    Code = "be",
                    Translators = new Person[] {
                        new Person { Name = "Alibical" }, // Chloe N.
                    }
                },
                new Language {
                    Name = "Български",
                    EnglishName = "Bulgarian",
                    Code = "bg",
                    Translators = new Person[] {
                        new Person { Name = "Nikolay Shangov (aka 21)", Url = "https://discord.gg/hqvMaxBAew" },
                        new Person { Name = "TheLocalSlavic" },
                        new Person { Name = "EmeraldCeat", Url = "https://discord.gg/reformedcityrp" },
                    }
                },
                new Language {
                    Name = "বাংলা",
                    EnglishName = "Bengali",
                    Code = "bn",
                    Translators = new Person[] {
                        new Person { Name = "mrimran", Url = "https://github.com/mr-Imran" },
                        new Person { Name = "BlackBox-cmd" },
                    }
                },
                new Language {
                    Name = "Bosanski",
                    EnglishName = "Bosnian",
                    Code = "bs",
                    Translators = new Person[] {
                        new Person { Name = "Trax" }, // Emin
                        new Person { Name = "Ammar" },
                    }
                },
                new Language {
                    Name = "Català",
                    EnglishName = "Catalan",
                    Code = "ca",
                    Translators = new Person[] {
                        DarlingChan,
                        new Person { Name = "Alte87" },
                    }
                },
                new Language {
                    Name = "Čeština",
                    EnglishName = "Czech",
                    Code = "cs",
                    Translators = new Person[] {
                        new Person { Name = "JayJake", Url = "https://jayjake.eu/" },
                        new Person { Name = "SunightMC" },
                        new Person { Name = "Tobias" }, // ***@***.cz
                        new Person { Name = "MakoPog" },
                        new Person { Name = "Sebastian" },
                    }
                },
                new Language {
                    Name = "Dansk",
                    EnglishName = "Danish",
                    Code = "da",
                    Translators = new Person[] {
                        new Person { Name = "Codiaz", Url = "https://codiaz.com/" },
                        new Person { Name = "Sebastian Hviid" },
                        new Person { Name = "Tobias" }, // ***@gmail.com
                        new Person { Name = "wimblim" },
                        new Person { Name = "David" }, // 0 translations
                        new Person { Name = "Johansenbastian6" }, // also 0
                    }
                },
                new Language {
                    Name = "Deutsch",
                    EnglishName = "German",
                    Code = "de",
                    Translators = new Person[] {
                        new Person { Name = "Ypsol", Url = "https://www.youtube.com/channel/UCxGqMDnXnEyVt4yugLeBpgA" },
                        MarcelGustin,
                        new Person { Name = "ahmad" },
                        new Person { Name = "binarynoise" },
                        new Person { Name = "Revax812" },
                        Yoshi,
                        new Person { Name = "Felix", Url = "https://github.com/fbrettnich" },
                        iRetrozx,
                        new Person { Name = "Tom" },
                    }
                },
                new Language {
                    Name = "Schwiizerdütsch",
                    EnglishName = "Swiss German",
                    Code = "de-ch",
                    Translators = new Person[] {
                        new Person { Name = "Foolian", Url = "https://foolian.com/" },
                        dragonGRaf,
                    }
                },
                new Language {
                    Name = "Ελληνικά",
                    EnglishName = "Greek",
                    Code = "el",
                    Translators = new Person[] {
                        new Person { Name = "Alex Grivas" },
                        new Person { Name = "NetworkTips#0001", Url = "https://discord.gg/Qb8RPjH6sD" }, // mrbeast
                    }
                },
                new Language {
                    Name = "English",
                    Code = "en"
                },
                new Language {
                    Name = "Español",
                    EnglishName = "Spanish",
                    Code = "es",
                    Translators = new Person[] {
                        new Person { Name = "Vexot" },
                        new Person { Name = "SirAmong" },
                        new Person { Name = "Pablo" },
                        DarlingChan,
                        new Person { Name = "UncleGeek" },
                        new Person { Name = "Luciousmc" },
                        new Person { Name = "Alvaro203204" },
                        new Person { Name = "JugandoMiguel", Url = "https://fiverr.com/jugandomiguel" },
                        new Person { Name = "Epic" }, // 0 translations
                    }
                },
                new Language {
                    Name = "Eesti",
                    EnglishName = "Estonian",
                    Code = "et",
                    Translators = new Person[] {
                        new Person { Name = "z1", Url = "https://github.com/Erkkii" },
                        Meelis,
                    }
                },
                new Language {
                    Name = "فارسی",
                    EnglishName = "Persian",
                    Code = "fa",
                    Translators = new Person[] {
                        new Person { Name = "Mohammad Mahdi", Url = "https://mo-mahdihh.ir/" },
                        new Person { Name = "Sunnystew" },
                    }
                },
                new Language {
                    Name = "Suomi",
                    EnglishName = "Finnish",
                    Code = "fi",
                    Translators = new Person[] {
                        new Person { Name = "Deluxeria" },
                        new Person { Name = "Zunikuu" },
                        new Person { Name = "jes", Url = "https://jesperiz.carrd.co/" }, // TurtleL
                        new Person { Name = "Super" },
                    }
                },
                new Language {
                    Name = "Filipino",
                    EnglishName = "Filipino",
                    Code = "fil",
                    Translators = new Person[] {
                        new Person { Name = "CtrlAltDelicious", Url = "https://www.youtube.com/c/CtrlAltDelicious_" },
                        new Person { Name = "jericko" },
                        new Person { Name = "Hachiki" },
                    }
                },
                new Language {
                    Name = "Français",
                    EnglishName = "French",
                    Code = "fr",
                    Translators = new Person[] {
                        new Person { Name = "Nenaff" },
                        new Person { Name = "RedNix" },
                        new Person { Name = "VaporCorp" }, // Account deleted
                    }
                },
                new Language {
                    Name = "Galego",
                    EnglishName = "Galician",
                    Code = "gl",
                    Translators = new Person[] {
                        new Person { Name = "Unknown" },
                    }
                },
                new Language {
                    Name = "עברית",
                    EnglishName = "Hebrew",
                    Code = "he",
                    Translators = new Person[] {
                        new Person { Name = "Galaxy6430", Url = "https://www.youtube.com/channel/UC_cnrLEXfwsZoQxEsM95HXg" },
                        new Person { Name = "Kahpot Vanilla", Url = "https://linktr.ee/KahpotVanilla" },
                        new Person { Name = "Amit" }
                    }
                },
                new Language {
                    Name = "हिन्दी",
                    EnglishName = "Hindi",
                    Code = "hi",
                    Translators = new Person[] {
                        new Person { Name = "regex", Url = "https://github.com/REGEX777" },
                        Julian,
                        new Person { Name = "mochiron desu" }
                    }
                },
                new Language {
                    Name = "Hrvatski",
                    EnglishName = "Croatian",
                    Code = "hr",
                    Translators = new Person[] {
                        new Person { Name = "Monika" },
                        iRetrozx,
                    }
                },
                new Language {
                    Name = "Magyar",
                    EnglishName = "Hungarian",
                    Code = "hu",
                    Translators = new Person[] {
                        new Person { Name = "Balla Botond", Url = "https://github.com/BallaBotond" },
                        new Person { Name = "Noxie" },
                        new Person { Name = "BunDzsi" },
                    }
                },
                new Language {
                    Name = "Bahasa Indonesia",
                    EnglishName = "Indonesian",
                    Code = "id",
                    Translators = new Person[] {
                        new Person { Name = "Hapnan" },
                        new Person { Name = "Apolycious" },
                        new Person { Name = "Bayu Sopwan", Url = "https://bayusopwan.github.io/" },
                        new Person { Name = "xChellz" },
                    }
                },
                new Language {
                    Name = "Íslenska",
                    EnglishName = "Icelandic",
                    Code = "is",
                    Translators = new Person[] {
                        new Person { Name = "phoenix" },
                        Meelis,
                        new Person { Name = "Amr Ezzat" },
                    }
                },
                new Language {
                    Name = "Italiano",
                    EnglishName = "Italian",
                    Code = "it",
                    Translators = new Person[] {
                        new Person { Name = "DJD320" },
                        new Person { Name = "Frin" },
                        new Person { Name = "Bay" },
                        Matthww,
                        new Person { Name = "ItsMrCube", Url = "https://mrcube.dev/" },
                        new Person { Name = "Patrick Canal" },
                    }
                },
                new Language {
                    Name = "日本語",
                    EnglishName = "Japanese",
                    Code = "ja",
                    Translators = new Person[] {
                        new Person { Name = "KABIKIRA000" },
                    }
                },
                new Language {
                    Name = "ქართული ენა",
                    EnglishName = "Georgian",
                    Code = "ka",
                    Translators = new Person[] {
                        new Person { Name = "Turashviliguro", Url = "https://turashviliguro.github.io/daddyexe/" },
                    }
                },
                new Language {
                    Name = "한국어",
                    EnglishName = "Korean",
                    Code = "ko",
                    Translators = new Person[] {
                        new Person { Name = "Yeongaori", Url = "https://github.com/yeongaori" },
                    }
                },
                new Language {
                    Name = "کوردی سۆرانی",
                    EnglishName = "Central Kurdish",
                    Code = "ku",
                    Translators = new Person[] {
                        new Person { Name = "SamTheNoob", Url = "https://linktr.ee/stn69" },
                        new Person { Name = "JakeAnthrax420" },
                    }
                },
                new Language {
                    Name = "Lietuvių",
                    EnglishName = "Lithuanian",
                    Code = "lt",
                    Translators = new Person[] {
                        GreenRosie,
                        new Person { Name = "Flix3ris" },
                    }
                },
                new Language {
                    Name = "Latviešu",
                    EnglishName = "Latvian",
                    Code = "lv",
                    Translators = new Person[] {
                        new Person { Name = "Buckneraaron07" },
                        new Person { Name = "Jaroslavs" },
                    }
                },
                new Language {
                    Name = "Македонски",
                    EnglishName = "Macedonian",
                    Code = "mk",
                    Translators = new Person[] {
                        falcon,
                    }
                },
                new Language {
                    Name = "မြန်မာဘာသာ",
                    EnglishName = "Burmese",
                    Code = "my",
                    Translators = new Person[] {
                        new Person { Name = "Bad Infer_#0069" },
                        new Person { Name = "BBbear#7149" }
                    }
                },
                new Language {
                    Name = "Nederlands",
                    EnglishName = "Dutch",
                    Code = "nl",
                    Translators = new Person[] {
                        new Person { Name = "Jeremyzijlemans", Url = "https://sionteam.com/" },
                        new Person { Name = "Screitsma64" },
                        new Person { Name = "sys-256", Url = "https://sys-256.me/" }, // Bramdevogel
                        new Person { Name = "DutchSlav" },
                    }
                },
                new Language {
                    Name = "Norsk",
                    EnglishName = "Norwegian",
                    Code = "no",
                    Translators = new Person[] {
                        new Person { Name = "Sveinung" },
                        new Person { Name = "Seth" },
                    }
                },
                new Language {
                    Name = "Polski",
                    EnglishName = "Polish",
                    Code = "pl",
                    Translators = new Person[] {
                        new Person { Name = "Lol1112345.lol12345" },
                        new Person { Name = "Liso" },
                        new Person { Name = "Piter" },
                        new Person { Name = "Oscar" },
                        MarcelGustin,
                        TofixRs,
                        new Person { Name = "KM127PL" },
                    }
                },
                new Language {
                    Name = "Português",
                    EnglishName = "Portuguese",
                    Code = "pt",
                    Translators = new Person[] {
                        new Person { Name = "Verygafanhot" },
                    }
                },
                new Language {
                    Name = "Português",
                    EnglishName = "Portuguese",
                    Dialect = "BR",
                    Code = "pt-br",
                    Translators = new Person[] {
                        new Person { Name = "Vinicio Henrique (viniciotricolor)" },
                        new Person { Name = "Slimakoi" },
                        new Person { Name = "Felipe B. Pansani" },
                        new Person { Name = "DeusDrizzyy" },
                        new Person { Name = "Leo" },
                    }
                },
                new Language {
                    Name = "Limba română",
                    EnglishName = "Romanian",
                    Code = "ro",
                    Translators = new Person[] {
                        new Person { Name = "DiDYRO", Url = "https://www.youtube.com/channel/UCjij9nYlEyPl5aVYnJkvx2w" },
                        new Person { Name = "inter", Url = "https://github.com/Electro7777" },
                        new Person { Name = "KTSGod", Url = "https://ktsgod.carrd.co/" }, // Developer316
                        new Person { Name = "Denisbolba" },
                        new Person { Name = "Eddie", Url = "https://github.com/EdiRo" },
                        new Person { Name = "veirvn" },
                        Matthww,
                        new Person { Name = "BlockBuzzters" },
                    }
                },
                new Language {
                    Name = "Русский",
                    EnglishName = "Russian",
                    Code = "ru",
                    Translators = new Person[] {
                        new Person { Name = "maximmax42", Url = "https://www.maximmax42.ru" },
                    }
                },
                new Language {
                    Name = "Slovenčina",
                    EnglishName = "Slovak",
                    Code = "sk",
                    Translators = new Person[] {
                        new Person { Name = "richi", Url = "https://e-z.bio/shelovesrichi" }, // rci1337
                        new Person { Name = "Maros Mihalek" },
                    }
                },
                new Language {
                    Name = "Slovenščina",
                    EnglishName = "Slovenian",
                    Code = "sl",
                    Translators = new Person[] {
                        new Person { Name = "BMKoscak" },
                    }
                },
                new Language {
                    Name = "Српски",
                    EnglishName = "Serbian",
                    Code = "sr",
                    Translators = new Person[] {
                        new Person { Name = "Vihaan" },
                        new Person { Name = "ToShibaToShamara" },
                        falcon,
                        new Person { Name = "Veljko" },
                    }
                },
                new Language {
                    Name = "Svenska",
                    EnglishName = "Swedish",
                    Code = "sv",
                    Translators = new Person[] {
                        new Person { Name = "leadattic_", Url = "https://leadattic.leadattic953788.repl.co/"}, // Axel
                        new Person { Name = "Rose Liljensten" },
                        new Person { Name = "James" },
                    }
                },
                new Language {
                    Name = "தமிழ்",
                    EnglishName = "Tamil",
                    Code = "ta",
                    Translators = new Person[] {
                        Julian,
                    }
                },
                new Language {
                    Name = "ภาษาไทย",
                    EnglishName = "Thai",
                    Code = "th",
                    Translators = new Person[] {
                        new Person { Name = "Squishee Freshy" },
                        new Person { Name = "YuuabyssSSID" },
                        new Person { Name = "SabbKor" },
                        new Person { Name = "OHMKUB" },
                        new Person { Name = "Game" }, //0 translations currently
                    }
                },
                new Language {
                    Name = "Türkçe",
                    EnglishName = "Turkish",
                    Code = "tr",
                    Translators = new Person[] {
                        new Person { Name = "Ozan Akyüz" },
                        new Person { Name = "josephisticated", Url = "https://github.com/josephisticated" },
                    }
                },
                new Language {
                    Name = "Українська",
                    EnglishName = "Ukrainian",
                    Code = "uk",
                    Translators = new Person[] {
                        new Person { Name = "MechaniX" },
                        new Person { Name = "Dmitromintenko" },
                        new Person { Name = "Desinger" },
                    }
                },
                new Language {
                    Name = "اُردُو",
                    EnglishName = "Urdu",
                    Code = "ur",
                    Translators = new Person[] {
                        new Person { Name = "xq34", Url = "https://github.com/S3ntryPositive" },
                        new Person { Name = "Muhammad Ali Ashraf" },
                    }
                },
                new Language {
                    Name = "Tiếng Việt",
                    EnglishName = "Vietnamese",
                    Code = "vi",
                    Translators = new Person[] {
                        Mykm,
                        new Person { Name = "Nhathungtran2011" },
                        new Person { Name = "Phnthnhnm0612" },
                        new Person { Name = "dsbachle" },
                        new Person { Name = "Ayano" },
                        new Person { Name = "03_Trần" },
                    }
                },
                new Language {
                    Name = "汉语",
                    EnglishName = "Simplified Chinese",
                    Code = "zh-hans",
                    Translators = new Person[] {
                        westxlu,
                        new Person { Name = "Zjsun.ca" },
                        new Person { Name = "zozocha" },
                    }
                },
                new Language {
                    Name = "漢語",
                    EnglishName = "Traditional Chinese",
                    Code = "zh-hant",
                    Translators = new Person[] {
                        westxlu,
                    }
                }
            };

            LanguageProgress = new Dictionary<string, string>();
            #region Generated code for progress
            LanguageProgress["ar"] = "100";
            LanguageProgress["be"] = "100";
            LanguageProgress["bn"] = "100";
            LanguageProgress["bs"] = "100";
            LanguageProgress["bg"] = "100";
            LanguageProgress["my"] = "94.95";
            LanguageProgress["ca"] = "100";
            LanguageProgress["zh-hans"] = "100";
            LanguageProgress["zh-hant"] = "100";
            LanguageProgress["hr"] = "100";
            LanguageProgress["cs"] = "100";
            LanguageProgress["da"] = "100";
            LanguageProgress["nl"] = "100";
            LanguageProgress["et"] = "100";
            LanguageProgress["fil"] = "100";
            LanguageProgress["fi"] = "100";
            LanguageProgress["fr"] = "100";
            LanguageProgress["gl"] = "100";
            LanguageProgress["ka"] = "100";
            LanguageProgress["de"] = "100";
            LanguageProgress["de-at"] = "0";
            LanguageProgress["de-ch"] = "100";
            LanguageProgress["el"] = "100";
            LanguageProgress["he"] = "100";
            LanguageProgress["hi"] = "98.99";
            LanguageProgress["hu"] = "100";
            LanguageProgress["is"] = "100";
            LanguageProgress["id"] = "100";
            LanguageProgress["it"] = "100";
            LanguageProgress["ja"] = "100";
            LanguageProgress["ko"] = "100";
            LanguageProgress["ku"] = "100";
            LanguageProgress["lv"] = "100";
            LanguageProgress["lt"] = "100";
            LanguageProgress["mk"] = "100";
            LanguageProgress["ml"] = "40.4";
            LanguageProgress["mn"] = "100";
            LanguageProgress["no"] = "100";
            LanguageProgress["fa"] = "100";
            LanguageProgress["pl"] = "100";
            LanguageProgress["pt"] = "100";
            LanguageProgress["pt-br"] = "100";
            LanguageProgress["ro"] = "100";
            LanguageProgress["ru"] = "100";
            LanguageProgress["sr"] = "100";
            LanguageProgress["sk"] = "100";
            LanguageProgress["sl"] = "100";
            LanguageProgress["es"] = "100";
            LanguageProgress["sv"] = "100";
            LanguageProgress["ta"] = "97.98";
            LanguageProgress["th"] = "100";
            LanguageProgress["tr"] = "100";
            LanguageProgress["uk"] = "100";
            LanguageProgress["ur"] = "100";
            LanguageProgress["vi"] = "100";
            #endregion

            Supporters = new List<Supporter>
            {
                new Supporter {
                    Name = "Grim",
                    Url = "https://www.savethekiwi.nz/",
                    USDAmount = "25.00",
                    AltAmount = "0.0008328 BTC"
                },
                new Supporter(
                    GreenRosie,
                    USDAmount: "6.00"
                ),
                new Supporter {
                    Name = "Boefjim",
                    Url = "https://boefjim.com/",
                    USDAmount = "16.13",
                    AltAmount = "1000.00 RUB"
                },
                new Supporter {
                    Name = "Anonymous",
                    USDAmount = "2.62",
                    AltAmount = "0.00011304 BTC"
                },
                new Supporter {
                    Name = "Eli404",
                    Url = "https://linktr.ee/404femboy",
                    USDAmount = "5.09",
                    AltAmount = "5.00 EUR"
                },
                new Supporter(
                    TofixRs,
                    USDAmount: "0.45",
                    AltAmount: "1.00 BAT"
                ),
                new Supporter {
                    Name = "kiwi",
                    USDAmount = "50.69",
                    AltAmount = "50.00 EUR"
                },
                new Supporter {
                    Name = "Kushgo",
                    Url = "https://opensea.io/collection/worldtowers",
                    USDAmount = "5.00",
                    AltAmount = "0.00313295 ETH"
                },
                new Supporter {
                    Name = "Bilal_786",
                    Url = "https://discord.gg/zabPuE78ne",
                    USDAmount = "5.79",
                    AltAmount = "5.80 EUR"
                },
                new Supporter(
                    Yoshi,
                    USDAmount: "3.28",
                    AltAmount: "200.00 RUB"
                ),
                new Supporter {
                    Name = "Zag",
                    Url = "https://zag.rip",
                    USDAmount = "7.45",
                    AltAmount = "431.55 RUB"
                },
                new Supporter(
                    dragonGRaf,
                    USDAmount: "30.00",
                    AltAmount: "1949.00 RUB"
                ),
                new Supporter {
                    Name = "DIGITAREZ SPACE", // Josenrique
                    Url = "https://digitarez.space/ref?customrp=default",
                    USDAmount = "9.15",
                    AltAmount = "617.00 RUB"
                },
                new Supporter {
                    Name = "YJB",
                    Url = "https://owo.yjb.gay/",
                    USDAmount = "0.69",
                    AltAmount = "41.00 RUB"
                },
                new Supporter {
                    Name = "Jan",
                    USDAmount = "1.49",
                    AltAmount = "100.00 RUB"
                },
                new Supporter {
                    Name = "CJPro25",
                    USDAmount = "0.91",
                    AltAmount = "63.00 RUB"
                },
                new Supporter {
                    Name = "WEIRON GREIZER",
                    USDAmount = "0.39",
                    AltAmount = "27.66 RUB"
                },
                new Supporter {
                    Name = "Death",
                    USDAmount = "1.93",
                    AltAmount = "142.00 RUB"
                },
                new Supporter {
                    Name = "RÏÇH KËÊD",
                    USDAmount = "2.10",
                    AltAmount = "30.00 TRX"
                },
                new Supporter (
                    Mykm,
                    USDAmount: "1.30",
                    AltAmount: "100.00 RUB"
                ),
                new Supporter {
                    Name = "BennyKun",
                    Url = "https://github.com/Benny-Kun",
                    USDAmount = "6.93",
                    AltAmount = "0.00023698 BTC"
                },
                new Supporter {
                    Name = "ZeroTheHero",
                    Url = "https://e-z.bio/zero",
                    USDAmount = "1.29",
                    AltAmount = "100.00 RUB"
                },
                new Supporter {
                    Name = "Maracesh",
                    Url = "https://lnk.bio/maracesh",
                    USDAmount = "1.32",
                    AltAmount = "100.00 RUB"
                },
            };

            NonMonetarySupporters = new List<NonMonetarySupporter>
            {
                new NonMonetarySupporter {
                    Name = "vouivre",
                    Url = "https://twitter.com/vvouivre",
                    DonationType = "A furry art",
                    DonationUrl = "https://cdn.discordapp.com/attachments/1028632852969033839/1028632881179922522/unknown.png"
                },
                new NonMonetarySupporter {
                    Name = "Sojpan",
                    Url = "https://twitter.com/Illeg__al",
                    DonationType = "A furry art",
                    DonationUrl = "https://cdn.discordapp.com/attachments/1092251869490978816/1092264289215184978/image.png"
                },
            };
        }

        /// <summary>
        /// A try-catch wrapper function for saving app settings.
        /// </summary>
        /// <returns><see langword="True"/> if settings were saved properly, <see langword="false"/> otherwise.</returns>
        public static bool SaveSettings()
        {
            try
            {
                Properties.Settings.Default.Save();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{Strings.errorSavingSettings} {ex.Message}", Strings.error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// A function to populate Settings -> Languages and Help -> Translators menus with languages and its translators.
        /// </summary>
        /// <param name="translatorsParent">Always <see cref="MainForm.translatorsToolStripMenuItem"/>.</param>
        /// <param name="translatorsHandler">Always <see cref="MainForm.OpenPersonsPage"/>.</param>
        /// <param name="languagesParent">Always <see cref="MainForm.languageToolStripMenuItem"/>.</param>
        /// <param name="languagesHandler">Always <see cref="MainForm.ChangeLanguage"/>.</param>
        public static void LanguagesSetup(ToolStripMenuItem translatorsParent, EventHandler translatorsHandler,
                                          ToolStripMenuItem languagesParent, EventHandler languagesHandler)
        {
            // Clearing out sample items I left in for design purposes
            translatorsParent.DropDownItems.Clear();
            languagesParent.DropDownItems.RemoveByKey("sampleToolStripMenuItem");

            foreach (var lang in Languages)
            {
                var dialect = "";
                if (!string.IsNullOrEmpty(lang.Dialect))
                    dialect = $" ({lang.Dialect})";

                // Populating Settings -> Languages
                languagesParent.DropDownItems.Add(new ToolStripMenuItem(lang.Name + dialect, null, languagesHandler)
                {
                    Tag = lang.Code,
                    ToolTipText = lang.EnglishName + dialect,
                });

                if (lang.Code == "en")
                    continue;

                // Populating Help -> Translators
                var langStrip = new ToolStripMenuItem($"{lang.Name} ({lang.EnglishName}){dialect} - {LanguageProgress[lang.Code]}%");

                foreach (var tl in lang.Translators)
                {
                    var tlItem = new ToolStripMenuItem(tl.Name, null, translatorsHandler);
                    if (!string.IsNullOrEmpty(tl.Url))
                    {
                        tlItem.Image = Properties.Resources.globe;
                        tlItem.Tag = ("translator", tl.Url);
                        tlItem.ToolTipText = tl.Url;
                    }
                    langStrip.DropDownItems.Add(tlItem);
                }

                translatorsParent.DropDownItems.Add(langStrip);
            }
        }

        /// <summary>
        /// A function to populate Help -> Supporter menu with supporters.
        /// </summary>
        /// <param name="parent">Always <see cref="MainForm.supportersToolStripMenuItem"/>.</param>
        /// <param name="handler">Always <see cref="MainForm.OpenPersonsPage"/>.</param>
        public static void SupportersSetup(ToolStripMenuItem parent, EventHandler handler)
        {
            // Clearing out sample item I left in for design purposes
            parent.DropDownItems.Clear();

            // Sort the supporters from higher to lower 
            Supporters.Sort((x, y) =>
            {
                float amtX = float.Parse(x.USDAmount, System.Globalization.CultureInfo.InvariantCulture);
                float amtY = float.Parse(y.USDAmount, System.Globalization.CultureInfo.InvariantCulture);

                return amtY.CompareTo(amtX);
            });

            // Populating Help -> Supporters
            foreach (var supporter in Supporters)
            {
                var altCur = "";
                if (!string.IsNullOrEmpty(supporter.AltAmount))
                    altCur = $" ({supporter.AltAmount})";

                var item = new ToolStripMenuItem($"{supporter.Name} - ${supporter.USDAmount}{altCur}", null, handler);

                if (!string.IsNullOrEmpty(supporter.Url))
                {
                    item.Image = Properties.Resources.globe;
                    item.Tag = ("supporter", supporter.Url);
                    item.ToolTipText = supporter.Url;
                }
                parent.DropDownItems.Add(item);
            }

            parent.DropDownItems.Add(new ToolStripSeparator());

            foreach (var nmSupporter in NonMonetarySupporters)
            {
                var item = new ToolStripMenuItem($"{nmSupporter.Name} - {nmSupporter.DonationType}", null, handler);

                if (!string.IsNullOrEmpty(nmSupporter.Url))
                {
                    item.Image = Properties.Resources.globe;
                    item.Tag = ("non-monetary supporter", nmSupporter.Url);
                    item.ToolTipText = nmSupporter.Url;
                }

                if (!string.IsNullOrEmpty(nmSupporter.DonationUrl))
                {
                    string txt = new System.ComponentModel.ComponentResourceManager(typeof(MainForm)).GetString("openToolStripMenuItem.Text");

                    item.DropDownItems.Add(new ToolStripMenuItem(txt, null, (o, e) => Process.Start(nmSupporter.DonationUrl))
                    {
                        ToolTipText = nmSupporter.DonationUrl
                    });
                }

                parent.DropDownItems.Add(item);
            }
        }
    }
}
