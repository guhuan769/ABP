/*
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2024-01-02 14:14:22
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2024-01-02 16:15:17
 * @FilePath: \universalmanagementsystem\src\main.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
import { createApp } from 'vue'
import './style.css'
import './assets/font/iconfont.css'
import 'element-plus/dist/index.css'//全局导入
import App from './App.vue'
import router from './router'
import {createPinia} from 'pinia'
const pinia = createPinia();

createApp(App).use(router).use(pinia).mount('#app')
