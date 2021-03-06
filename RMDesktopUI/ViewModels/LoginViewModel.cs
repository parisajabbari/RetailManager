﻿using Caliburn.Micro;
using RMDesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        //Private backing fields
        private string _userName;
        private string _password;
        private IAPIHelper _apiHelper;
        public LoginViewModel(IAPIHelper apiHelper)
        {

            _apiHelper = apiHelper;

        }

        //Public properties
        public string UserName
        {
            get { return _userName; }
            set 
            { 
                _userName = value;
                NotifyOfPropertyChange(() => UserName);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public string Password
        {
            get { return _password; }
            set 
            { 
                _password = value;
                NotifyOfPropertyChange(() => Password);
                NotifyOfPropertyChange(() => CanLogIn);
            }
        }

        public bool IsErrorVisible
        {
            get {
                    bool output = false;
                    if(_errorMessage?.Length > 0)
                    {
                        output = true;
                    }
                    return output; 
                }

        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set {
                //The order of commands matter here. As timing to chnage the error message and notifying
                    _errorMessage = value;
                    NotifyOfPropertyChange(() => ErrorMessage);
                    NotifyOfPropertyChange(() => IsErrorVisible);
                     
                }
        }



        public bool CanLogIn
        {
            get{
                bool output = false;

                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }

                return output;
            }

        }

        public async Task LogIn( )
        {
            try
            {
                ErrorMessage = "";
                var result = await _apiHelper.Authenticate(UserName, Password);
                //Todo: here we need to put a bearer token so we know the info of the user like username and token.

            }
            catch (Exception ex)
            {

                //throw;
                ErrorMessage = ex.Message;
            }        
        }


    }
}
