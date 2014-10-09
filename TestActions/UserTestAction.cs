namespace TestActions
{
    #region using

    using Microsoft.Practices.Unity;

    #endregion

    public class UserTestAction<TIUser> where TIUser : class
    {
        private readonly UnityContainer _unityContainer = new UnityContainer();

        private TIUser _currentUser;

        /// <summary>
        /// Возвращает исходный экземпляр Unity-контейнера.
        /// </summary>
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
            var userName = GetUserName<TUser>();
            _unityContainer.RegisterType<TIUser, TUser>(userName, new ContainerControlledLifetimeManager());
            return this;
        }

        /// <summary>
        /// Задать текущего пользователя.
        /// </summary>
        /// <param name="user">
        /// Экземпляр пользователя.
        /// </param>
        public UserTestAction<TIUser> SetCurrentUser(TIUser user)
        {
            _currentUser = user;
            return this;
        }

        /// <summary>
        /// Задать текущего пользователя по его типу.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public UserTestAction<TIUser> SetCurrentUser<TUser>()
            where TUser : TIUser
        {
            _currentUser = GetUser<TUser>();
            return this;
        }

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        public TIUser GetUser()
        {
            if (_currentUser == null)
            {
                throw new TestActionsException("Не задан текущий пользователь.");
            }

            return _currentUser;
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
            return PrivateResolve<TAction>(_currentUser);
        }

        /// <summary>
        /// Возвращает действие от заданного по типу пользователя (должен быть зарегистрированным).
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

        /// <summary>
        /// Возвращает действие от заданного пользователя.
        /// </summary>
        /// <param name="user">
        /// Экземпляр пользователя.
        /// </param>
        /// <typeparam name="TAction">
        /// Тип действия.
        /// </typeparam>
        public TAction Resolve<TAction>(TIUser user)
        {
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
            if (!_unityContainer.IsRegistered<TIUser>(userName))
            {
                throw new TestActionsException(string.Format("Пользователь \"{0}\" незарегистрирован.", userName));
            }

            return _unityContainer.Resolve<TIUser>(userName);
        }

        private TAction PrivateResolve<TAction>(TIUser user)
        {
            return _unityContainer.Resolve<TAction>(new DependencyOverride(typeof(TIUser), user));
        }
    }
}
