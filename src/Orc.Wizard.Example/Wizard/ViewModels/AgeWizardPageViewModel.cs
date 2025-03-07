namespace Orc.Wizard.Example.Wizard.ViewModels;

using System;
using System.Linq;
using System.Threading.Tasks;
using Catel;
using Catel.MVVM;
using Catel.Services;

public class AgeWizardPageViewModel : WizardPageViewModelBase<AgeWizardPage>
{
    private readonly IMessageService _messageService;

    public AgeWizardPageViewModel(AgeWizardPage wizardPage, IMessageService messageService)
        : base(wizardPage)
    {
        ArgumentNullException.ThrowIfNull(messageService);

        _messageService = messageService;

        AddPage = new TaskCommand(OnAddPageExecuteAsync);
    }

    [ViewModelToModel]
    public string Age { get; set; }

    public TaskCommand AddPage { get; private set; }

    private async Task OnAddPageExecuteAsync()
    {
        if ((await _messageService.ShowAsync("Would you like to add a new page to see the dynamic navigation pane behavior?", "Add page?", MessageButton.YesNo, MessageImage.Question)) != MessageResult.Yes)
        {
            return;
        }

        Wizard.InsertPage<AgeWizardPage>(WizardPage.Number);
    }

    /// <summary>
    /// 从上一步骤获取的数据
    /// </summary>
    public ISummaryItem PersonSummary { get; private set; }

    protected override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        var wizard = Wizard;
        if (wizard is null)
        {
            return;
        }

        foreach (var page in wizard.Pages)
        {
            // Skip pages that were not visited
            if (!page.IsVisited) continue;

            var summary = page.GetSummary();
            if (summary is null) continue;

            if (summary.Page is null)
            {
                summary.Page = page;
            }

            if (summary.Title == "Person")
            {
                PersonSummary = summary;
                break;
            }
        }
    }
}
