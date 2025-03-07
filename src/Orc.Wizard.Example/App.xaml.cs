namespace Orc.Wizard.Example;

using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Catel.IoC;
using Catel.Logging;
using Catel.Reflection;
using Catel.Services;
using Orc.Theming;
using Orc.Wizard.Example.Wizard;
using Orchestra;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IAccentColorService _accentColorService;
    private IBaseColorSchemeService _baseColorSchemeService;
    private Color _selectedAccentColor;
    private string _selectedBaseColorScheme;

    private static readonly ILog Log = LogManager.GetCurrentClassLogger();

    protected override void OnStartup(StartupEventArgs e)
    {
#if DEBUG
        LogManager.AddDebugListener(true);
#endif

        _accentColorService = ServiceLocator.Default.ResolveType<IAccentColorService>();
        var AccentColors = typeof(Colors).GetPropertiesEx(true, true)
       .Where(x => x.PropertyType.IsAssignableFromEx(typeof(Color)))
       .Select(x => (Color?)x.GetValue(null))
       .Where(x => x is not null)
       .Cast<Color>()
       .ToList();

        var currentAccentColor = Orc.Theming.ThemeManager.Current.GetAccentColorBrush().Color;
        if (!AccentColors.Contains(currentAccentColor))
        {
            AccentColors.Insert(0, currentAccentColor);
        }
        _selectedAccentColor = currentAccentColor;

        _baseColorSchemeService = ServiceLocator.Default.ResolveType<IBaseColorSchemeService>();
        _baseColorSchemeService.BaseColorSchemeChanged += OnBaseColorSchemeServiceBaseColorSchemeChanged;
        _selectedBaseColorScheme = _baseColorSchemeService.GetBaseColorScheme();
        _baseColorSchemeService.SetBaseColorScheme(_selectedBaseColorScheme);

        var themeManager = ControlzEx.Theming.ThemeManager.Current;
        themeManager.RegisterLibraryThemeProvider(new LibraryThemeProvider());
        themeManager.SyncTheme();

        var languageService = ServiceLocator.Default.ResolveType<ILanguageService>();


        // Note: it's best to use .CurrentUICulture in actual apps since it will use the preferred language
        // of the user. But in order to demo multilingual features for devs (who mostly have en-US as .CurrentUICulture),
        // we use .CurrentCulture for the sake of the demo
        languageService.PreferredCulture = CultureInfo.CurrentCulture;
        languageService.FallbackCulture = new CultureInfo("zh-Hans");

        StyleHelper.CreateStyleForwardersForDefaultStyles();

        this.ApplyTheme();

        var wizardService = ServiceLocator.Default.ResolveType<IWizardService>();
        var typeFactory = ServiceLocator.Default.ResolveType<ITypeFactory>();

        //直接启动 ExampleWizard
        var wizard = typeFactory.CreateInstance<ExampleWizard>();
        wizard.AllowQuickNavigationWrapper = true;
        wizard.HandleNavigationStatesWrapper = true;
        wizard.CacheViewsWrapper = true;
        wizard.ShowPageHeaderWrapper = true;

        wizardService.ShowWizardAsync(wizard);


        base.OnStartup(e);
    }
    private void OnBaseColorSchemeServiceBaseColorSchemeChanged(object sender, EventArgs e)
    {
        _selectedBaseColorScheme = _baseColorSchemeService.GetBaseColorScheme();
    }
}
