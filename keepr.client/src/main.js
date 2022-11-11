import '@mdi/font/css/materialdesignicons.css'

import Popper from "vue3-popper";
import 'bootstrap'
import { createApp } from 'vue'

// @ts-ignore
import App from './App.vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import { router } from './router'
import { VueMasonryPlugin } from "vue-masonry";
const root = createApp(App)
registerGlobalComponents(root)


root.use(router).use(VueMasonryPlugin).mount("#app");
