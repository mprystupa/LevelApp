const LocalStorageService = (function() {
  var _service;
  function _getService() {
    if (!_service) {
      _service = this;
      return _service;
    }
    return _service;
  }
  function _setToken(tokenObj) {
    localStorage.setItem("accessToken", tokenObj.accessToken);
    localStorage.setItem("refreshToken", tokenObj.refreshToken);
  }
  function _getAccessToken() {
    return localStorage.getItem("accessToken");
  }
  function _getRefreshToken() {
    return localStorage.getItem("refreshToken");
  }
  function _clearToken() {
    localStorage.removeItem("accessToken");
    localStorage.removeItem("refreshToken");
  }
  function _setLockedLessonsIds(lockedLessonsIds) {
    localStorage.setItem("lockedLessons", lockedLessonsIds);
  }
  function _getLockedLessonsIds() {
    let ids = localStorage.getItem("lockedLessons").split(",");

    if (ids && ids[0] !== "") {
      return ids;
    } else {
      return [];
    }
  }
  function _clearLockedLessonsIds() {
    localStorage.removeItem("lockedLessons");
  }
  return {
    getService: _getService,
    setToken: _setToken,
    getAccessToken: _getAccessToken,
    getRefreshToken: _getRefreshToken,
    clearToken: _clearToken,
    setLockedLessonsIds: _setLockedLessonsIds,
    getLockedLessonsIds: _getLockedLessonsIds,
    clearLockedLessonsIds: _clearLockedLessonsIds
  };
})();

export default LocalStorageService;
