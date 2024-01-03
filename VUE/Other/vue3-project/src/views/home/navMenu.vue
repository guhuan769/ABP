<!--
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2023-12-29 16:41:40
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-12-30 17:00:27
 * @FilePath: \vue3-project\src\views\home\navMenu.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<!--
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2023-12-29 16:41:40
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-12-30 15:43:29
 * @FilePath: \vue3-project\src\views\home\navMenu.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->

<template>
   <el-aside width="200px" class="aside"> 
        <el-menu
            default-active="2"
            class="el-menu-vertical-demo" router
        >
            <!-- <el-menu-item :index="v.url" v-for="v in items" :key="v.url">
                <el-icon><component :is="v.icon"></component></el-icon>
                <span>{{v.title}}</span>
            </el-menu-item>
            -->
           
           <NavItem v-for="v in items" :key="v.url" :item="v" />

        </el-menu>
    </el-aside>
</template>

<script setup>
import {reactive, ref} from 'vue'
import {
  Document,
  Menu as IconMenu,
  Location,
  Setting,
} from '@element-plus/icons-vue'
import NavItem from './navItem.vue'

const title = ref('首页')
const items =reactive([
    {title:'首页',url:'/index',icon:Document},
    {
        title:'用户管理',
        url:'/user',
        icon:Location,
        child:[
            {title:'用户详情',url:'/user/info',icon:Document},
            {title:'用户分析',url:'/user/analyse',icon:IconMenu},
        ]
    },
    {
        title:'设备监控',
        url:'/device',
        icon:Document,
        child:[
            {title:'设备列表',url:'/device/list',icon:Document},
            {title:'设备管理',url:'/device/manage',icon:IconMenu},
            {title:'设备维修',url:'/device/repair',icon:Document},
        ]
    },
    {title:'统计分析',url:'/census',icon:Setting},
    {
        title:'平台配置',
        url:'/platform',
        icon:Document,
        child:[
            {title:'代码生成器',url:'/platform/codeGeneration',icon:Document},
        ]
    },
])

</script>