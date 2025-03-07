namespace Orc.Wizard.Example.Wizard;

/// <summary>
/// 示例向导接口
/// </summary>
public interface IExampleWizard : IWizard
{
    /// <summary>
    /// 是否允许快速导航
    /// </summary>
    bool AllowQuickNavigationWrapper { get; set; }
    /// <summary>
    /// 是否处理导航状态
    /// </summary>
    bool HandleNavigationStatesWrapper { get; set; }
    /// <summary>
    /// 导航控制器
    /// </summary>
    INavigationController NavigationControllerWrapper { get; set; }
    /// <summary>
    /// 是否显示帮助
    /// </summary>
    bool ShowHelpWrapper { get; set; }
    /// <summary>
    /// 是否显示步骤页标题
    /// </summary>
    bool ShowPageHeaderWrapper { get; set; }
    /// <summary>
    /// 是否在任务栏显示
    /// </summary>
    bool ShowInTaskbarWrapper { get; set; }
    /// <summary>
    /// 是否缓存视图
    /// </summary>
    bool CacheViewsWrapper { get; set; }
    /// <summary>
    /// 是否自动调整侧面导航窗格的大小
    /// </summary>
    bool AutoSizeSideNavigationPaneWrapper { get; set; }
}
