// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import $ from 'jquery' ;

import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import '../node_modules/bootstrap/dist/js/bootstrap.min.js';
Vue.config.productionTip = false

/* eslint-disable no-new */
let vm=new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>'
})