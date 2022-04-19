mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, worldaa!");
  },
  Clear: function () {
    var connection = window.indexedDB.open('idbfs', 1);
    connection.onsuccess = (e) => {
      var database = e.target.result;
      var transaction = database.transaction(['FILE_DATA']);
      var objectStore = transaction.objectStore('FILE_DATA');
      //delete by key
      objectStore.delete('/idbfs/f44724bff49fcbabcf91d9187327bf0e/UnityCache/Shared/ab');


    }
  },
});