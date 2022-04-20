mergeInto(LibraryManager.library, {

    Hello: function () {
        window.alert("Hello, worldaa!");
    },
    Clear: function (nameDB, pathDB) {
        nameDB = UTF8ToString(nameDB);
        pathDB = UTF8ToString(pathDB);
        console.log("Clear: " + nameDB + " " + pathDB);
        var db = window.indexedDB.open(nameDB); //success
        db.onsuccess = function (event) {
            db = event.target.result;
            console.log('db clear', db);
            var transaction = db.transaction(['FILE_DATA'], 'readwrite');
            var objectStore = transaction.objectStore('FILE_DATA');
            //get all keys from object store
            var getAllKeysRequest = objectStore.getAllKeys();
            getAllKeysRequest.onsuccess = function (event) {
                var keys = event.target.result;
                console.log('keys', keys);
                keys.forEach(function (key) {
                    if (key.includes(pathDB)) {
                        console.log('key', key);
                        objectStore.delete(key);
                    }

                })

            }

        }

    },
});