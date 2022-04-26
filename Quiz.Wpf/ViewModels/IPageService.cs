using System.Threading.Tasks;

namespace Quiz.Wpf.ViewModels;

public interface IPageService
{
    Task ChangePageAsync<TViewModel>() where TViewModel : PageViewModelBase;
    
    Task ChangePageAsync<TViewModel, TParameter>(TParameter parameter) 
        where TViewModel : InitializablePageViewModelBase<TParameter>
        where TParameter : class;
}