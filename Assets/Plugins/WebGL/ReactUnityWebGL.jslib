mergeInto(LibraryManager.library, {
  Login: function (_address, _token) {
    try {
      dispatchReactUnityEvent("Login", _address, _token);
    }catch(e) {

    }
   
  },
  ScoreUpdate: function (_score) {
     try {
     dispatchReactUnityEvent("ScoreUpdate", _score);
    }catch(e) {

    }
    
  },
  Roll: function () {
    try {
      dispatchReactUnityEvent("Roll");
    }catch(e) {

    }
   
  },
});
