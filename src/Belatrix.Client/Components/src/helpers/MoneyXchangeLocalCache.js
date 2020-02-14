export default class MoneyXchangeLocalCache {
    constructor(key) {
        if (!window.localStorage) {
            throw 'MoneyXchangeLocalCache is not supported ..';
        }

        if (!key) {
            throw 'Key was not provided ..';
        }

        this._key = key.toUpperCase() + '.';
    }

    _prepareKey(key) {
        return this._key + key;
    }

    get(key) {
        key = this._prepareKey(key);

        let result = localStorage.getItem(key);

        if (result) {
            result = JSON.parse(result);

            if (!result.expire) {
                return result.value;
            }

            if (result.expire >= new Date().getTime()) {
                return result.value;
            }

            localStorage.removeItem(key);
        }

        return null;
    }

    add(key, value, time = null) {
        key = this._prepareKey(key);

        // object to store
        let item = {
            key,
            value,
            expire: _fromTime(time)
        };

        localStorage.setItem(key, JSON.stringify(item));

        // time logic
        function _fromTime(time) {
            if (!time) {
                return null;
            }

            let date = new Date();

            date.setMinutes(date.getMinutes() + time);

            return date.getTime();
        }
    }

    clear() {
        for (let key in localStorage) {
            if (key.includes(this._key)) {
                localStorage.removeItem(key);
            }
        }
    }

    delete(key) {
        key = this._prepareKey(key);
        localStorage.removeItem(key);
    }

    credits() {
        console.log('Developed by Kodoti.com');
    }
}