export default {
  name: "exchangerate",
  props: {

  },
  mounted() {
    let self = this;
    self.getCurrency();
  },
  data() {
    return {
      from: null,
      to: null,
      currency: {
        code: 'EUR',
        rate: 0
      },
      hasFocus: true,
      isLoading: false,
      cache: {
        key: 'currency',
        minutes: 1
      }
    };
  },
  computed: {

  },
  methods: {
    calculate() {
      let self = this;
      self.to = self.from * self.currency.rate;
    },
    getCurrency() {
      let self = this;

      // Check if exists in cache
      let obj = self.$helpers.cache.get(self.cache.key);

      if (obj) {
        // Set currency rate
        self.currency.rate = obj.rate;
      } else {
        // Make a request to our server
        _proxy();
      }

      function _proxy() {
        // Enable loading
        self.isLoading = true;

        self.$proxies.currencyProxy.get(self.currency.code).then(r => {
          // Disable loading
          self.isLoading = false;

          // Set currency rate
          self.currency.rate = r.data.value;

          // Set to cache for 10 minutes
          self.$helpers.cache.add(self.cache.key, self.currency, self.cache.minutes);
        }).catch(e => {
          self.isLoading = false;
          console.error(e);
        })
      }
    },
    addOrRemoveFocus(v) {
      let self = this;

      if (v) {
        setTimeout(() => {
          self.$refs.currency.focus();
        }, 50);
      } else if (!self.from) {
        return;
      }

      self.hasFocus = v;
    }
  },
  filters: {
    currency(value, code) {
      return new Intl.NumberFormat('en-EN', {
        style: 'currency',
        currency: code,
        maximumFractionDigits: 4,
        minimumFractionDigits: 4,
      }).format(value);
    }
  }
};