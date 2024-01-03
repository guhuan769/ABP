/*
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2023-12-29 15:29:34
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-12-30 17:51:23
 * @FilePath: \vue3-project\src\router\index.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
import {createRouter,createWebHashHistory} from 'vue-router'


const routes = [
    {
        path:'/',
        redirect:'/home'  //重定向
    },
    // {
    //     path:'/login',
    //     name:'login',
    //     component:()=>import('')
    // },
    {
        path:'/home',  //路径
        name:'home',  //名字 唯一
        meta: {title:'首页'}, //布局也
        redirect:'/index',  //重定向
        component:()=>import('../views/home/index.vue'),
        children:[
            {
                path:'/index',  //首页
                name:'index',
                meta: {title:'首页'}, //
                component:()=>import('../views/home/index/index.vue')
            },
            {
                path:'/user',  //用户管理
                name:'user',
                meta: {title:'用户管理'}, //
                component:()=>import('../views/home/user/index.vue'),
                children:[
                    {
                        path:'/user/info', //用户详情
                        name:'info',
                        component:()=>import('../views/home/user/info/index.vue'),
                        meta: {title:'用户详情'}, //
                    },
                    {
                        path:'/user/analyse', //用户分析
                        name:'analyse',
                        component:()=>import('../views/home/user/analyse/index.vue'),
                        meta: {title:'用户分析'}, //
                    },
                ]

            },
            {
                path:'/device',  //设备监控
                name:'device',
                component:()=>import('../views/home/device/index.vue'),
                meta: {title:'设备监控'}, //
                children:[
                    {
                        path:'/device/list', //设备列表
                        name:'list',
                        component:()=>import('../views/home/device/list/index.vue'),
                        meta: {title:'设备列表'}, //
                    },
                    {
                        path:'manage', //设备管理
                        name:'manage',
                        component:()=>import('../views/home/device/manage/index.vue'),
                        meta: {title:'设备管理'}, //
                    },
                    {
                        path:'repair', //设备维修
                        name:'repair',
                        component:()=>import('../views/home/device/repair/index.vue'),
                        meta: {title:'设备维修'}, //
                    },
                ]
            },
            {
                path:'/census',  //统计分析
                name:'census',
                component:()=>import('../views/home/census/index.vue'),
                meta: {title:'统计分析'}, //
            },
            {
                path:'/platform',  //平台配置
                name:'platform',
                component:()=>import('../views/home/platform/index.vue'),
                meta: {title:'平台配置'}, //
                children:[
                    {
                        path:'/platform/codeGeneration', //代码生成器
                        name:'codeGeneration',
                        component:()=>import('../views/home/platform/codeGeneration/index.vue'),
                        meta: {title:'代码生成器'}, //
                    },
                ]
            },
        ]
    }
]

const router = createRouter({
    history:createWebHashHistory(),  //hash
    routes
})

export default router