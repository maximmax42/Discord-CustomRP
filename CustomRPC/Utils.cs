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
        /// Percentage of the translation progress.
        /// </summary>
        public string Progress;
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
    /// Utility functions.
    /// </summary>
    public static class Utils
    {
        /// <summary>
        /// List of languages and their translators used to populate Settings -> Languages and Help -> Translators menu items.<br/>
        /// It is done this way because visual designer doesn't work properly with a lot of items.
        /// </summary>
        static List<Language> Languages => new List<Language> {
            new Language {
                Name = "مَصرى",
                EnglishName = "Arabic",
                Dialect = "EG",
                Code = "ar-eg",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "FiberAhmed", Url = "https://github.com/FiberAhmed" },
                    new Translator { Name = "ShadowlGamer" }
                }
            },
            new Language {
                Name = "Čeština",
                EnglishName = "Czech",
                Code = "cs",
                Progress = "96.81",
                Translators = new Translator[] {
                    new Translator { Name = "JayJake", Url = "https://jayjake.eu/" },
                    new Translator { Name = "Tobias" }
                }
            },
            new Language {
                Name = "Dansk",
                EnglishName = "Danish",
                Code = "da",
                Progress = "95.74",
                Translators = new Translator[] {
                    new Translator { Name = "Sebastian Hviid" },
                    new Translator { Name = "Tobias" },
                    new Translator { Name = "David Gjøtterup Malmstrøm" },
                }
            },
            new Language {
                Name = "Deutsche",
                EnglishName = "German",
                Code = "de",
                Progress = "95.74",
                Translators = new Translator[] {
                    new Translator { Name = "Ypsol", Url = "https://www.youtube.com/channel/UCxGqMDnXnEyVt4yugLeBpgA" },
                    new Translator { Name = "ahmad" },
                    new Translator { Name = "binarynoise" },
                    new Translator { Name = "Felix", Url = "https://github.com/fbrettnich" },
                }
            },
            new Language {
                Name = "Ελληνικά",
                EnglishName = "Greek",
                Code = "el",
                Progress = "95.74",
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
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Vexot" },
                    new Translator { Name = "Pablo" },
                    new Translator { Name = "Luciousmc" },
                    new Translator { Name = "Alvaro203204" },
                    new Translator { Name = "Epic" },
                }
            },
            new Language {
                Name = "Français",
                EnglishName = "French",
                Code = "fr",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Nenaff" },
                }
            },
            new Language {
                Name = "עברית",
                EnglishName = "Hebrew",
                Code = "he",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Galaxy6430", Url = "https://www.youtube.com/channel/UC_cnrLEXfwsZoQxEsM95HXg" },
                    new Translator { Name = "Kahpot Vanilla", Url = "https://linktr.ee/KahpotVanilla" },
                    new Translator { Name = "Amit" }
                }
            },
            new Language {
                Name = "Magyar",
                EnglishName = "Hungarian",
                Code = "hu",
                Progress = "91.49",
                Translators = new Translator[] {
                    new Translator { Name = "Balla Botond", Url = "https://github.com/BallaBotond" },
                }
            },
            new Language {
                Name = "Hrvatski",
                EnglishName = "Croatian",
                Code = "hr",
                Progress = "74.47",
                Translators = new Translator[] {
                    new Translator { Name = "Monika" },
                }
            },
            new Language {
                Name = "Bahasa Indonesia",
                EnglishName = "Indonesian",
                Code = "id",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Hapnan" },
                    new Translator { Name = "Apolycious" },
                    new Translator { Name = "Bayu Sopwan", Url = "https://bayusopwan.github.io/" },
                }
            },
            new Language {
                Name = "Italiano",
                EnglishName = "Italian",
                Code = "it",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "DJD320" },
                    new Translator { Name = "Frin" },
                    new Translator { Name = "ItsMrCube", Url = "https://mrcube.live/" },
                }
            },
            new Language {
                Name = "日本語",
                EnglishName = "Japanese",
                Code = "ja",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "KABIKIRA000" },
                }
            },
            new Language {
                Name = "한국어",
                EnglishName = "Korean",
                Code = "ko",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Yeongaori", Url = "https://github.com/yeongaori" },
                }
            },
            new Language {
                Name = "کوردی سۆرانی",
                EnglishName = "Central Kurdish",
                Code = "ku",
                Progress = "97.87",
                Translators = new Translator[] {
                    new Translator { Name = "SamTheNoob", Url = "https://linktr.ee/stn69" },
                }
            },
            new Language {
                Name = "Lietuvių",
                EnglishName = "Lithuanian",
                Code = "lt",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "White Rose", Url = "https://www.twitch.tv/white_rose_lt" },
                }
            },
            new Language {
                Name = "မြန်မာဘာသာ",
                EnglishName = "Burmese",
                Code = "my",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Infernite#0680" },
                    new Translator { Name = "BBbear#7149" }
                }
            },
            new Language {
                Name = "Nederlands",
                EnglishName = "Dutch",
                Code = "nl",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Jeremyzijlemans", Url = "https://sionhub.co.uk/" },
                }
            },
            new Language {
                Name = "Polski",
                EnglishName = "Polish",
                Code = "pl",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Lol1112345.lol12345" },
                    new Translator { Name = "Liso" },
                    new Translator { Name = "Piter" },
                }
            },
            new Language {
                Name = "Português",
                EnglishName = "Portuguese",
                Dialect = "BR",
                Code = "pt-br",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Vinicio Henrique (viniciotricolor)" },
                    new Translator { Name = "Slimakoi" },
                    new Translator { Name = "DeusDrizzyy" },
                }
            },
            new Language {
                Name = "Limba română",
                EnglishName = "Romanian",
                Code = "ro",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "DiDYRO", Url = "https://www.youtube.com/channel/UCjij9nYlEyPl5aVYnJkvx2w" },
                    new Translator { Name = "Denisbolba" },
                }
            },
            new Language {
                Name = "Русский",
                EnglishName = "Russian",
                Code = "ru",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "maximmax42", Url = "https://www.maximmax42.ru" },
                }
            },
            new Language {
                Name = "ภาษาไทย",
                EnglishName = "Thai",
                Code = "th",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Squishee Freshy" },
                    new Translator { Name = "Game" },
                }
            },
            new Language {
                Name = "Türkçe",
                EnglishName = "Turkish",
                Code = "tr",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Ozan Akyüz" },
                    new Translator { Name = "Yusuf" },
                }
            },
            new Language {
                Name = "Українська",
                EnglishName = "Ukrainian",
                Code = "uk",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "MechaniX" },
                    new Translator { Name = "Dmitromintenko" },
                }
            },
            new Language {
                Name = "Tiếng Việt",
                EnglishName = "Vietnamese",
                Code = "vi",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Mykm", Url = "https://github.com/yumiruuwu" },
                    new Translator { Name = "Phnthnhnm0612" },
                    new Translator { Name = "dsbachle" },
                    new Translator { Name = "03_Trần" },
                }
            },
            new Language {
                Name = "汉语",
                EnglishName = "Simplified Chinese",
                Code = "zh-hans",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "Zjsun.ca" },
                    new Translator { Name = "zozocha" },
                }
            },
            new Language {
                Name = "漢語",
                EnglishName = "Traditional Chinese",
                Code = "zh-hant",
                Progress = "100",
                Translators = new Translator[] {
                    new Translator { Name = "蘆筍 (westxlu)", Url = "https://linktr.ee/westxlu" },
                }
            }
        };

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
        /// <param name="translatorsHandler">Always <see cref="MainForm.OpenTranslatorPage"/>.</param>
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
                    Tag = lang.Code
                });

                if (lang.Code == "en")
                    continue;

                // Populating Help -> Translators
                var langStrip = new ToolStripMenuItem($"{lang.Name} ({lang.EnglishName}){dialect} - {lang.Progress}%");

                foreach (var tl in lang.Translators)
                {
                    var tlItem = new ToolStripMenuItem(tl.Name, null, translatorsHandler);
                    if (!string.IsNullOrEmpty(tl.Url))
                    {
                        tlItem.Image = Properties.Resources.globe;
                        tlItem.Tag = tl.Url;
                    }
                    langStrip.DropDownItems.Add(tlItem);
                }

                translatorsParent.DropDownItems.Add(langStrip);
            }
        }
    }
}
