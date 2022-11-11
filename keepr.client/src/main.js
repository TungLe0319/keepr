import '@mdi/font/css/materialdesignicons.css'
import mitt from "mitt";
import Popper from "vue3-popper";
import 'bootstrap'
import { createApp } from 'vue'
import 'masonry-layout'
import 'vue-masonry'
// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'
import { VueMasonryPlugin } from "vue-masonry";
const root = createApp(App)
registerGlobalComponents(root)
const emitter = mitt();


root.use(router).use(VueMasonryPlugin).mount("#app");
