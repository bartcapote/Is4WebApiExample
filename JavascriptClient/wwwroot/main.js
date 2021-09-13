var config = {
    userStore: new Oidc.WebStorageStateStore({ store: window.localStorage }), // use local storage instead of session storage
    authority: "https://localhost:44324/",
    client_id: "client_id_js",
    response_type: "code", // instead of "id_token token" because of PKCE
    redirect_uri: "https://localhost:44366/Home/SignIn",
    scope: "openid MyApiOne Blob my.api.claim my.scope" // TODO Iss1, not-todo: Iss3
};

var userManager = new Oidc.UserManager(config);

var signIn = function () {
    userManager.signinRedirect();
};

userManager.getUser().then(user => {
    console.log("user:", user); // TODO delete this
    if(user) {
        axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
    }
});

var callApi = function () {
    axios.get("https://localhost:44358/secret")
        .then(result => {
            console.log(result);
            refreshing = false;
        });
};

var refreshing = false;        

// axios http middleware. token refresh. DO NOT use oidc-client-js automaticSilentRenew flag, it's deprecated and buggy.
axios.interceptors.response.use(
    function(response) { return response; },
    async function(error) {
        var userInStore = await userManager.getUser().then(user => user != null);
        console.error("axios intercepted error: ", error.response); // TODO delete this
        
        var axiosConfig = error.response.config;
        
        // if error response is 401 try to refresh token
        if (error.response.status === 401 && userInStore) {
            console.error("access token expired");
            
            // if already refreshing don't make another request
            if (!refreshing) {
                console.log("starting token refresh"); // TODO delete this

                // maybe catch required login here? something like session expired?

                refreshing = true;

                // do the refresh
                return userManager.signinSilent().then(user => {
                    console.log("new user: ", user) // TODO delete this

                    // update the http request and axios client
                    axios.defaults.headers.common["Authorization"] = "Bearer " + user.access_token;
                    axiosConfig.headers["Authorization"] = "Bearer " + user.access_token;

                    // retry the http request
                    return axios(axiosConfig);
                }); // TODO Iss 4 make this signinSilentCallback() - needs a dedicated silenSignIn page in SPA
            }
        }

        return Promise.reject(error)
    }
);