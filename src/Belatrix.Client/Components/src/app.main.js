// Vue
import Vue from 'vue';

// Helpers
import helpers from './helpers/config';
helpers(Vue);

// Proxies
import proxies from './proxies/config';
proxies(Vue);

window.Vue = Vue;

// Components
import exchangerate from './components/exchangerate/exchangerate.vue';

window.Components = [];
window.Components[exchangerate.name] = exchangerate;