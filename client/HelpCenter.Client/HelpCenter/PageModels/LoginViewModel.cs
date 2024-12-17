using Common.Models.Request;
using Common.Models.Response;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HelpCenter.Data.Http;
using Refit;

namespace HelpCenter.PageModels;

public partial class LoginViewModel : ObservableObject
{

    public IUserApi _userData { get; }

    public LoginViewModel(IUserApi userData)
    {
        _userData = userData;

    }
    [ObservableProperty] private string userLoginName = string.Empty;
    [ObservableProperty] private string userPassword = string.Empty;
    [ObservableProperty] private string loginMessage = string.Empty;
    [ObservableProperty] private bool turnLoginMessage = false;

    //[ObservableProperty] private bool turnLoginMessage => !string.IsNullOrWhiteSpace(loginMessage);

    [ObservableProperty] private bool isBusy = false;
    [ObservableProperty] private bool isVisible = true;
    partial void OnIsBusyChanged(bool oldValue, bool newValue) => IsVisible = !newValue;

    //public async Task GetDataFromPrefernces()
    //{
    //    //DataSaver.ClearAll();
    //    IsBusy = true;
    //    try
    //    {
    //        string data = Preferences.Default.Get<string>(nameof(UserLoginDto), null);
    //        if (data == null) return;

    //        var user = JsonSerializer.Deserialize<UserLoginDto>(data);

    //        InfoModel er = await _userData.Login(user);
    //        if (er == null)
    //        {
    //            await Shell.Current.GoToAsync("//MainPage");
    //        }
    //        else
    //        {
    //            LoginMessage = er.message;
    //            TurnLoginMessage = true;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        await Shell.Current.DisplayAlert("LoginViewModel", ex.Message, "ok");
    //    }
    //    finally
    //    {
    //        IsBusy = false;
    //    }

    //}




    [RelayCommand]
    private async Task GoToMainPage()
    {
        IsBusy = true;

        UserMainInfo user = new UserMainInfo { email = UserLoginName, password = UserPassword };
        try
        {
            var er = await _userData.Login(user);
            await Shell.Current.GoToAsync("//main");
            //if (er == null)
            //{
            //    Preferences.Default.Set(nameof(UserLoginDto), JsonSerializer.Serialize(user));
            //}
            //else
            //{
            //    LoginMessage = er.message;
            //    TurnLoginMessage = true;
            //}
        }
        catch (ApiException ex)
        {
            ErrorResponse? error = await ex.Err();
            LoginMessage = error?.GetError;
        }
        catch (Exception ex)
        {
            LoginMessage = ex.Message;
            TurnLoginMessage = true;
        }


        IsBusy = false;
    }


    [RelayCommand]
    private void GoToCreateAccount(object obj)
    {
        // obj is null?
        //Shell.Current.GoToAsync(nameof(CreateAccountPage));
    }

    [RelayCommand]
    private void GoToForgotPassword(object obj)
    {
        //Shell.Current.GoToAsync(nameof(ResetPasswordPage));
    }


}


