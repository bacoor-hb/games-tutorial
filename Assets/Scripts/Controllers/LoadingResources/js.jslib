mergeInto(LibraryManager.library, {
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
    GetKeyIndexedDB: function (pTimerCallback) {
        console.log("[JavaScript] GetKeyIndexedDB");

        var iteration = 0;
        var intervalID = setInterval(timerCallback, 1000);

        function timerCallback() {
            iteration++;
            var GetKeys = new Promise(function (resolve, reject) {
                var db = window.indexedDB.open("/idbfs");
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

                        resolve(keys);


                    }
                }

            });
            GetKeys.then(function (keys) {
                var timeString = keys.toString();
                var bufferSize = lengthBytesUTF8(timeString) + 1;
                var buffer = _malloc(bufferSize);
                stringToUTF8(timeString, buffer, bufferSize);

                // _iii signature corresponds to `bool (int, string)`, because
                // a bool is represented by an integer with value 0 or 1,
                // and a string is also represented by an integer (a pointer to the allocated string)
                var continueIterating = dynCall_iii(pTimerCallback, iteration, buffer);

                // free the allocated time string
                _free(buffer);

                // we can stop iterating depending on the value returned by the callback
                if (!continueIterating)
                    clearInterval(intervalID);
            })

        }
    },


});