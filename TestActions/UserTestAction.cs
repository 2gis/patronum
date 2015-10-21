namespace TestActions
{
    #region using

    using Microsoft.Practices.Unity;

    #endregion

    public class UserTestAction<TIUser>
        where TIUser : class
    {
        #region Fields

        private readonly UnityContainer unityContainer = new UnityContainer();

        private TIUser currentUser;

        #endregion

        #region Public Properties

        /// <summary>
        /// Возвращает исходный экземпляр Unity-контейнера.
        /// </summary>
        public UnityContainer Instance
        {
            get
            {
                return this.unityContainer;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Возвращает текущего пользователя.
        /// </summary>
        public TIUser GetUser()
        {
            if (this.currentUser == null)
            {
                throw new TestActionsException("Не задан текущий пользователь.");
            }

            return this.currentUser;
        }

        /// <summary>
        /// Возвращает пользователя по его типу.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public TIUser GetUser<TUser>() where TUser : TIUser
        {
            var userName = this.GetUserName<TUser>();
            return this.PrivateGetUser(userName);
        }

        /// <summary>
        /// Возвращает пользователя по его имени.
        /// </summary>
        /// <param name="userName">
        /// Имя пользователя.
        /// </param>
        public TIUser GetUser(string userName)
        {
            return this.PrivateGetUser(userName);
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public UserTestAction<TIUser> RegisterUser<TUser>() where TUser : TIUser
        {
            var userName = this.GetUserName<TUser>();
            this.unityContainer.RegisterType<TIUser, TUser>(userName, new ContainerControlledLifetimeManager());
            return this;
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="userName">
        /// Имя пользователя.
        /// </param>
        /// <param name="user">
        /// Экземпляр пользователя.
        /// </param>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public UserTestAction<TIUser> RegisterUser<TUser>(string userName, TUser user) where TUser : TIUser
        {
            this.unityContainer.RegisterInstance<TIUser>(userName, user, new ContainerControlledLifetimeManager());
            return this;
        }

        /// <summary>
        /// Возвращает действие от текущего пользователя.
        /// </summary>
        /// <typeparam name="TAction">
        /// Тип действия.
        /// </typeparam>
        public TAction Resolve<TAction>()
        {
            return this.PrivateResolve<TAction>(this.currentUser);
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
        public TAction Resolve<TAction, TUser>() where TUser : TIUser
        {
            var user = this.GetUser<TUser>();
            return this.PrivateResolve<TAction>(user);
        }

        /// <summary>
        /// Возвращает действие от заданного по имени пользователя (должен быть зарегистрированным).
        /// </summary>
        /// <param name="userName">
        /// Имя пользователя.
        /// </param>
        /// <typeparam name="TAction">
        /// Тип действия.
        /// </typeparam>
        public TAction Resolve<TAction>(string userName)
        {
            var user = this.GetUser(userName);
            return this.PrivateResolve<TAction>(user);
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
            return this.PrivateResolve<TAction>(user);
        }

        /// <summary>
        /// Задать текущего пользователя.
        /// </summary>
        /// <param name="user">
        /// Экземпляр пользователя.
        /// </param>
        public UserTestAction<TIUser> SetCurrentUser(TIUser user)
        {
            this.currentUser = user;
            return this;
        }

        /// <summary>
        /// Задать текущего пользователя по его типу.
        /// </summary>
        /// <typeparam name="TUser">
        /// Тип пользователя.
        /// </typeparam>
        public UserTestAction<TIUser> SetCurrentUser<TUser>() where TUser : TIUser
        {
            this.currentUser = this.GetUser<TUser>();
            return this;
        }

        /// <summary>
        /// Задать текущего пользователя по его имени.
        /// </summary>
        /// <param name="userName">
        /// Имя пользователя.
        /// </param>
        public UserTestAction<TIUser> SetCurrentUser(string userName)
        {
            this.currentUser = this.GetUser(userName);
            return this;
        }

        #endregion

        #region Methods

        private string GetUserName<TUser>() where TUser : TIUser
        {
            var userType = typeof(TUser);
            return userType.Name;
        }

        private TIUser PrivateGetUser(string userName)
        {
            if (!this.unityContainer.IsRegistered<TIUser>(userName))
            {
                throw new TestActionsException(string.Format("Пользователь \"{0}\" незарегистрирован.", userName));
            }

            return this.unityContainer.Resolve<TIUser>(userName);
        }

        private TAction PrivateResolve<TAction>(TIUser user)
        {
            return this.unityContainer.Resolve<TAction>(new DependencyOverride(typeof(TIUser), user));
        }

        #endregion
    }
}
