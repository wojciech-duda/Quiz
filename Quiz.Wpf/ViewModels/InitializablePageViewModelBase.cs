namespace Quiz.Wpf.ViewModels;

public abstract class InitializablePageViewModelBase<TParameter> : PageViewModelBase
{
    public abstract void Initialize(TParameter parameter);
}