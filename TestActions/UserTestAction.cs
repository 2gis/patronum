namespace TestActions
{
    #region using

    using Microsoft.Practices.Unity;

    using TestActions.Exceptions;

    #endregion

    public class UserTestAction<TIUser>
    {
        private readonly UnityContainer _unityContainer = new UnityContainer();

        private string _currentUserName = string.Empty;

        public UnityContainer Instance
        {
            get
            {
                return _unityContainer;
            }
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public UserTestAction<TIUser> RegisterUser<TUser>()
            where TUser : TIUser
        {
            var userName = GetUserName<TIUser>();
            _unityContainer.RegisterType<TIUser, TUser>(userName, new ContainerControlledLifetimeManager());
            return this;
        }

        /// <summary>
        /// Задать текущего пользователя.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public UserTestAction<TIUser> SetCurrentUser<TUser>()
            where TUser : TIUser
        {
            _currentUserName = GetUserName<TUser>();
            return this;
        }

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        public TIUser GetUser()
        {
            if (string.IsNullOrEmpty(_currentUserName))
            {
                throw new TestActionsException("Не задан текущий пользователь.");
            }

            return PrivateGetUser(_currentUserName);
        }

        /// <summary>
        /// Возвращает пользователя по его типу.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public TIUser GetUser<TUser>()
            where TUser : TIUser
        {
            var userName = GetUserName<TUser>();
            return PrivateGetUser(userName);
        }

        /// <summary>
        /// Возвращает действие от текущего пользователя.
        /// </summary>
        /// <typeparam name="TAction">
        /// Тип действия.
        /// </typeparam>
        public TAction Resolve<TAction>()
        {
            var user = GetUser();
            return PrivateResolve<TAction>(user);
        }

        /// <summary>
        /// Возвращает действие от заданного пользователя (должен быть зарегистрированным).
        /// </summary>
        /// <typeparam name="TAction">
        /// Тип действия.
        /// </typeparam>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public TAction Resolve<TAction, TUser>()
            where TUser : TIUser
        {
            var user = GetUser<TUser>();
            return PrivateResolve<TAction>(user);
        }

        private string GetUserName<TUser>()
            where TUser : TIUser
        {
            var userType = typeof(TUser);
            return userType.Name;
        }

        private TIUser PrivateGetUser(string userName)
        {
            return _unityContainer.Resolve<TIUser>(userName);
        }

        private TAction PrivateResolve<TAction>(TIUser user)
        {
            return _unityContainer.Resolve<TAction>(new DependencyOverride(typeof(TIUser), user));
        }
    }
}
