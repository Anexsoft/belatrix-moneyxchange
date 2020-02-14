export default function CurrencyProxy(axios) {
    return {
        get(code) {
            return axios.get(`/currencies/${code}`);
        }
    }
}