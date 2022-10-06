using System;
using System.Collections.Generic;
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
    /// A struct describing a translator.
    /// </summary>
    struct Translator
    {
        /// <summary>
        /// Nickname of the translator.
        /// </summary>
        public string Name;
        /// <summary>
        /// URL of the translator, optional.
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
        public Translator[] Translators;
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

        static Utils()
        {
            Languages = new List<Language> {
                new Language {
                    Name = "مَصرى",
                    EnglishName = "Arabic",
                    Code = "ar",
                    Translators = new Translator[] {
                        new Translator { Name = "FiberAhmed", Url = "https://github.com/FiberAhmed" },
                        new Translator { Name = "ShadowlGamer" },
                        new Translator { Name = "karimawi" },
                    }
                },
                new Language {
                    Name = "Български",
                    EnglishName = "Bulgarian",
                    Code = "bg",
                    Translators = new Translator[] {
                        new Translator { Name = "TheLocalSlavic" },
                        new Translator { Name = "EmeraldCeat", Url = "https://discord.gg/reformedcityrp" },
                    }
                },
                new Language {
                    Name = "বাংলা",
                    EnglishName = "Bengali",
                    Code = "bn",
                    Translators = new Translator[] {
                        new Translator { Name = "mrimran", Url = "https://github.com/mr-Imran" },
                    }
                },
                new Language {
                    Name = "Català",
                    EnglishName = "Catalan",
                    Code = "ca",
                    Translators = new Translator[] {
                        new Translator { Name = "Darling-Chan", Url = "https://meap.gg/" },
                    }
                },
                new Language {
                    Name = "Čeština",
                    EnglishName = "Czech",
                    Code = "cs",
                    Translators = new Translator[] {
                        new Translator { Name = "JayJake", Url = "https://jayk.live/" },
                        new Translator { Name = "SunightMC" },
                        new Translator { Name = "Tobias" },
                        new Translator { Name = "MakoPog" }
                    }
                },
                new Language {
                    Name = "Dansk",
                    EnglishName = "Danish",
                    Code = "da",
                    Translators = new Translator[] {
                        new Translator { Name = "Sebastian Hviid" },
                        new Translator { Name = "Tobias" },
                        new Translator { Name = "wimblim" },
                        new Translator { Name = "David" },
                        new Translator { Name = "Johansenbastian6" },
                    }
                },
                new Language {
                    Name = "Deutsche",
                    EnglishName = "German",
                    Code = "de",
                    Translators = new Translator[] {
                        new Translator { Name = "Ypsol", Url = "https://www.youtube.com/channel/UCxGqMDnXnEyVt4yugLeBpgA" },
                        new Translator { Name = "ahmad" },
                        new Translator { Name = "Marcel Gustin", Url = "https://marcelgustin.de" },
                        new Translator { Name = "Yoshi" },
                        new Translator { Name = "binarynoise" },
                        new Translator { Name = "Felix", Url = "https://github.com/fbrettnich" },
                        new Translator { Name = "Tom" },
                    }
                },
                new Language {
                    Name = "Ελληνικά",
                    EnglishName = "Greek",
                    Code = "el",
                    Translators = new Translator[] {
                        new Translator { Name = "Alex Grivas" },
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
                    Translators = new Translator[] {
                        new Translator { Name = "Vexot" },
                        new Translator { Name = "SirAmong" },
                        new Translator { Name = "Pablo" },
                        new Translator { Name = "Darling-Chan", Url = "https://meap.gg/" },
                        new Translator { Name = "Luciousmc" },
                        new Translator { Name = "Alvaro203204" },
                        new Translator { Name = "Epic" },
                    }
                },
                new Language {
                    Name = "فارسی",
                    EnglishName = "Persian",
                    Code = "fa",
                    Translators = new Translator[] {
                        new Translator { Name = "Mohammad Mahdi", Url = "https://mo-mahdihh.ir/" },
                    }
                },
                new Language {
                    Name = "Suomi",
                    EnglishName = "Finnish",
                    Code = "fi",
                    Translators = new Translator[] {
                        new Translator { Name = "Deluxeria" },
                        new Translator { Name = "Zunikuu" },
                        new Translator { Name = "TurtleL" },

                    }
                },
                new Language {
                    Name = "Filipino",
                    EnglishName = "Filipino",
                    Code = "fil",
                    Translators = new Translator[] {
                        new Translator { Name = "CtrlAltDelicious", Url = "https://www.youtube.com/c/CtrlAltDelicious_" },
                        new Translator { Name = "jericko" },
                        new Translator { Name = "Hachiki" },
                    }
                },
                new Language {
                    Name = "Français",
                    EnglishName = "French",
                    Code = "fr",
                    Translators = new Translator[] {
                        new Translator { Name = "Nenaff" },
                        new Translator { Name = "RedNix" },
                        new Translator { Name = "VaporCorp" }, // Account deleted
                    }
                },
                new Language {
                    Name = "עברית",
                    EnglishName = "Hebrew",
                    Code = "he",
                    Translators = new Translator[] {
                        new Translator { Name = "Galaxy6430", Url = "https://www.youtube.com/channel/UC_cnrLEXfwsZoQxEsM95HXg" },
                        new Translator { Name = "Kahpot Vanilla", Url = "https://linktr.ee/KahpotVanilla" },
                        new Translator { Name = "Amit" }
                    }
                },
                new Language {
                    Name = "हिन्दी",
                    EnglishName = "Hindi",
                    Code = "hi",
                    Translators = new Translator[] {
                        new Translator { Name = "regex", Url = "https://github.com/REGEX777" },
                        new Translator { Name = "Julian", Url = "https://discord.com/oauth2/authorize?client_id=962323485772881950&scope=bot&permissions=8" },
                        new Translator { Name = "mochiron desu" }
                    }
                },
                new Language {
                    Name = "Hrvatski",
                    EnglishName = "Croatian",
                    Code = "hr",
                    Translators = new Translator[] {
                        new Translator { Name = "Monika" },
                    }
                },
                new Language {
                    Name = "Magyar",
                    EnglishName = "Hungarian",
                    Code = "hu",
                    Translators = new Translator[] {
                        new Translator { Name = "Balla Botond", Url = "https://github.com/BallaBotond" },
                        new Translator { Name = "Noxie" },
                        new Translator { Name = "BunDzsi" },
                    }
                },
                new Language {
                    Name = "Bahasa Indonesia",
                    EnglishName = "Indonesian",
                    Code = "id",
                    Translators = new Translator[] {
                        new Translator { Name = "Hapnan" },
                        new Translator { Name = "Apolycious" },
                        new Translator { Name = "Bayu Sopwan", Url = "https://bayusopwan.github.io/" },
                        new Translator { Name = "xChellz" },
                    }
                },
                new Language {
                    Name = "Italiano",
                    EnglishName = "Italian",
                    Code = "it",
                    Translators = new Translator[] {
                        new Translator { Name = "DJD320" },
                        new Translator { Name = "Frin" },
                        new Translator { Name = "Matthww" },
                        new Translator { Name = "ItsMrCube", Url = "https://mrcube.live/" },
                        new Translator { Name = "Bay" },
                        new Translator { Name = "Patrick Canal" },
                    }
                },
                new Language {
                    Name = "日本語",
                    EnglishName = "Japanese",
                    Code = "ja",
                    Translators = new Translator[] {
                        new Translator { Name = "KABIKIRA000" },
                    }
                },
                new Language {
                    Name = "ქართული ენა",
                    EnglishName = "Georgian",
                    Code = "ka",
                    Translators = new Translator[] {
                        new Translator { Name = "Turashviliguro" },
                    }
                },
                new Language {
                    Name = "한국어",
                    EnglishName = "Korean",
                    Code = "ko",
                    Translators = new Translator[] {
                        new Translator { Name = "Yeongaori", Url = "https://github.com/yeongaori" },
                    }
                },
                new Language {
                    Name = "کوردی سۆرانی",
                    EnglishName = "Central Kurdish",
                    Code = "ku",
                    Translators = new Translator[] {
                        new Translator { Name = "SamTheNoob", Url = "https://linktr.ee/stn69" },
                    }
                },
                new Language {
                    Name = "Lietuvių",
                    EnglishName = "Lithuanian",
                    Code = "lt",
                    Translators = new Translator[] {
                        new Translator { Name = "GreenRosie", Url = "https://www.twitch.tv/greenrosie" },
                    }
                },
                new Language {
                    Name = "မြန်မာဘာသာ",
                    EnglishName = "Burmese",
                    Code = "my",
                    Translators = new Translator[] {
                        new Translator { Name = "Bad Infer_#0069" },
                        new Translator { Name = "BBbear#7149" }
                    }
                },
                new Language {
                    Name = "Nederlands",
                    EnglishName = "Dutch",
                    Code = "nl",
                    Translators = new Translator[] {
                        new Translator { Name = "Jeremyzijlemans", Url = "https://sionteam.com/" },
                        new Translator { Name = "Screitsma64" },
                        new Translator { Name = "sys-256", Url = "https://sys-256.me/" }, // Bramdevogel
                    }
                },
                new Language {
                    Name = "Norsk",
                    EnglishName = "Norwegian",
                    Code = "no",
                    Translators = new Translator[] {
                        new Translator { Name = "Sveinung" },
                    }
                },
                new Language {
                    Name = "Polski",
                    EnglishName = "Polish",
                    Code = "pl",
                    Translators = new Translator[] {
                        new Translator { Name = "Lol1112345.lol12345" },
                        new Translator { Name = "Liso" },
                        new Translator { Name = "Piter" },
                        new Translator { Name = "Oscar" },
                        new Translator { Name = "Marcel Gustin", Url = "https://marcelgustin.de" },
                        new Translator { Name = "Tofix.rs" },
                    }
                },
                new Language {
                    Name = "Português",
                    EnglishName = "Portuguese",
                    Code = "pt",
                    Translators = new Translator[] {
                        new Translator { Name = "Verygafanhot" },
                    }
                },
                new Language {
                    Name = "Português",
                    EnglishName = "Portuguese",
                    Dialect = "BR",
                    Code = "pt-br",
                    Translators = new Translator[] {
                        new Translator { Name = "Vinicio Henrique (viniciotricolor)" },
                        new Translator { Name = "Slimakoi" },
                        new Translator { Name = "Felipe B. Pansani" },
                        new Translator { Name = "DeusDrizzyy" },
                        new Translator { Name = "Leo" },
                    }
                },
                new Language {
                    Name = "Limba română",
                    EnglishName = "Romanian",
                    Code = "ro",
                    Translators = new Translator[] {
                        new Translator { Name = "DiDYRO", Url = "https://www.youtube.com/channel/UCjij9nYlEyPl5aVYnJkvx2w" },
                        new Translator { Name = "Denisbolba" },
                        new Translator { Name = "KTSGod", Url = "https://ktsgod.carrd.co/" },
                        new Translator { Name = "Eddie", Url = "https://github.com/EdiRo" },
                        new Translator { Name = "Matthww" },
                    }
                },
                new Language {
                    Name = "Русский",
                    EnglishName = "Russian",
                    Code = "ru",
                    Translators = new Translator[] {
                        new Translator { Name = "maximmax42", Url = "https://www.maximmax42.ru" },
                    }
                },
                new Language {
                    Name = "Српски",
                    EnglishName = "Serbian",
                    Code = "sr",
                    Translators = new Translator[] {
                        new Translator { Name = "Vihaan" },
                        new Translator { Name = "ToShibaToShamara" },
                        new Translator { Name = "Veljko" },
                    }
                },
                new Language {
                    Name = "Svenska",
                    EnglishName = "Swedish",
                    Code = "sv",
                    Translators = new Translator[] {
                        new Translator { Name = "leadattic_", Url = "https://leadattic.leadattic953788.repl.co/"}, // Axel
                        new Translator { Name = "Rose Liljensten" },
                        new Translator { Name = "James" },
                    }
                },
                new Language {
                    Name = "தமிழ்",
                    EnglishName = "Tamil",
                    Code = "ta",
                    Translators = new Translator[] {
                        new Translator { Name = "Julian", Url = "https://discord.com/oauth2/authorize?client_id=962323485772881950&scope=bot&permissions=8" },
                    }
                },
                new Language {
                    Name = "ภาษาไทย",
                    EnglishName = "Thai",
                    Code = "th",
                    Translators = new Translator[] {
                        new Translator { Name = "Squishee Freshy" },
                        new Translator { Name = "YuuabyssSSID" },
                        new Translator { Name = "SabbKor" },
                        //new Translator { Name = "Game" }, 0 translations currently?
                    }
                },
                new Language {
                    Name = "Türkçe",
                    EnglishName = "Turkish",
                    Code = "tr",
                    Translators = new Translator[] {
                        new Translator { Name = "Ozan Akyüz" },
                        new Translator { Name = "josephisticated", Url = "https://github.com/josephisticated" },
                    }
                },
                new Language {
                    Name = "Українська",
                    EnglishName = "Ukrainian",
                    Code = "uk",
                    Translators = new Translator[] {
                        new Translator { Name = "MechaniX" },
                        new Translator { Name = "Dmitromintenko" },
                    }
                },
                new Language {
                    Name = "Tiếng Việt",
                    EnglishName = "Vietnamese",
                    Code = "vi",
                    Translators = new Translator[] {
                        new Translator { Name = "Mykm", Url = "https://github.com/yumiruuwu" },
                        new Translator { Name = "Phnthnhnm0612" },
                        new Translator { Name = "dsbachle" },
                        new Translator { Name = "Ayano" },
                        new Translator { Name = "03_Trần" },
                    }
                },
                new Language {
                    Name = "汉语",
                    EnglishName = "Simplified Chinese",
                    Code = "zh-hans",
                    Translators = new Translator[] {
                        new Translator { Name = "蘆筍 (westxlu)", Url = "https://linktr.ee/westxlu" },
                        new Translator { Name = "Zjsun.ca" },
                        new Translator { Name = "zozocha" },
                    }
                },
                new Language {
                    Name = "漢語",
                    EnglishName = "Traditional Chinese",
                    Code = "zh-hant",
                    Translators = new Translator[] {
                        new Translator { Name = "蘆筍 (westxlu)", Url = "https://linktr.ee/westxlu" },
                    }
                }
            };

            LanguageProgress = new Dictionary<string, string>();
            #region Generated code for progress
            LanguageProgress["ar"] = "100";
            LanguageProgress["bn"] = "97.98";
            LanguageProgress["bg"] = "98.99";
            LanguageProgress["my"] = "94.95";
            LanguageProgress["ca"] = "100";
            LanguageProgress["zh-hans"] = "100";
            LanguageProgress["zh-hant"] = "100";
            LanguageProgress["hr"] = "70.71";
            LanguageProgress["cs"] = "100";
            LanguageProgress["da"] = "98.99";
            LanguageProgress["nl"] = "98.99";
            LanguageProgress["et"] = "79.8";
            LanguageProgress["fil"] = "100";
            LanguageProgress["fi"] = "100";
            LanguageProgress["fr"] = "100";
            LanguageProgress["ka"] = "100";
            LanguageProgress["de"] = "100";
            LanguageProgress["el"] = "90.91";
            LanguageProgress["he"] = "100";
            LanguageProgress["hi"] = "98.99";
            LanguageProgress["hu"] = "100";
            LanguageProgress["id"] = "98.99";
            LanguageProgress["it"] = "98.99";
            LanguageProgress["ja"] = "100";
            LanguageProgress["ko"] = "100";
            LanguageProgress["ku"] = "92.93";
            LanguageProgress["lv"] = "40.4";
            LanguageProgress["lt"] = "97.98";
            LanguageProgress["no"] = "98.99";
            LanguageProgress["fa"] = "100";
            LanguageProgress["pl"] = "98.99";
            LanguageProgress["pt"] = "100";
            LanguageProgress["pt-br"] = "100";
            LanguageProgress["ro"] = "100";
            LanguageProgress["ru"] = "100";
            LanguageProgress["sr"] = "98.99";
            LanguageProgress["es"] = "100";
            LanguageProgress["sv"] = "98.99";
            LanguageProgress["ta"] = "97.98";
            LanguageProgress["th"] = "98.99";
            LanguageProgress["tr"] = "100";
            LanguageProgress["uk"] = "100";
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
                new Supporter {
                    Name = "GreenRosie",
                    Url = "https://www.twitch.tv/greenrosie",
                    USDAmount = "6.00",
                },
                new Supporter {
                    Name = "Boefjim",
                    Url = "https://boefjim.com/",
                    USDAmount = "16.13",
                    AltAmount = "1000 RUB"
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
                    AltAmount = "5 EUR"
                },
                new Supporter {
                    Name = "Tofix.rs",
                    USDAmount = "0.45",
                    AltAmount = "1 BAT"
                },
                new Supporter {
                    Name = "kiwi",
                    USDAmount = "50.69",
                    AltAmount = "50 EUR"
                },
                new Supporter {
                    Name = "Kushgo",
                    USDAmount = "5.00",
                    AltAmount = "0.00313295 ETH"
                },
                new Supporter {
                    Name = "Bilal_786",
                    Url = "https://discord.gg/zabPuE78ne",
                    USDAmount = "5.79",
                    AltAmount = "5.80 EUR"
                },
                new Supporter {
                    Name = "Yoshi",
                    USDAmount = "3.28",
                    AltAmount = "200.00 RUB"
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
        }
    }
}
