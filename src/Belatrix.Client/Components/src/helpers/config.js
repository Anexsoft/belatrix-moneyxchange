import MoneyXchangeLocalCache from '../helpers/MoneyXchangeLocalCache';

export default function (Vue) {
    Vue.use({
        install(Vue) {
            Object.defineProperty(Vue.prototype, '$helpers', {
                value: {
                    cache: new MoneyXchangeLocalCache('moneyxchange')
                }
            });
        }
    });
};