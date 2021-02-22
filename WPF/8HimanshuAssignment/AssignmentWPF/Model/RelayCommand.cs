using System;
using System.Windows.Input;

namespace AssignmentWPF.Model
{
    /// <summary>
    /// Common class for Command implementation
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private members
        private Action<object> execute;
        private Func<object, bool> canExecute;
        #endregion

        #region Events
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// comman can be executed only if canExecute returns true
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }
        /// <summary>
        /// Action invoker
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            execute(parameter);
        }
        #endregion

        #region Constructor
        public RelayCommand(Action<object> action, Func<object, bool> canCommandExecute = null)
        {
            execute = action;
            canExecute = canCommandExecute;
        } 
        #endregion
    }
}
