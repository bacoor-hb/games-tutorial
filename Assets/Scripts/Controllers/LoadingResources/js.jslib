mergeInto(LibraryManager.library, {

    GetAllCacheAssetBundle: function (nameDB) {
        nameDB = UTF8ToString(nameDB);
        var db = window.indexedDB.open(nameDB);
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
                var returnStr = "bla";
                var bufferSize = lengthBytesUTF8(returnStr) + 1;
                var buffer = _malloc(bufferSize);
                stringToUTF8(returnStr, buffer, bufferSize);
                return buffer;

            }


        }

    },
    Clear: function (nameDB, pathDB) {
        nameDB = UTF8ToString(nameDB);
        pathDB = UTF8ToString(pathDB);
        console.log("Clear: " + nameDB + " " + pathDB);
        var db = window.indexedDB.open(nameDB);
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
    ReceiveString: function () {
      
           
                var str = "A string passed from JavaScript to C#";
                var bufferSize = lengthBytesUTF8(str) + 1; // calculate the size of null-terminated UTF-8 string
                var buffer = _malloc(bufferSize); // allocate string buffer on the heap
                stringToUTF8(str, buffer, bufferSize); // fill the buffer with the string UTF-8 value
              
            
              return buffer; // return the pointer of the allocated string to C#


        }
    
    },

});