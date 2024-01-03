<!--
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2023-12-29 16:41:40
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-12-30 17:16:58
 * @FilePath: \vue3-project\src\views\home\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
   <div id="module">
    <el-container>
         <!-- 左侧导航 -->
         <NavMenu />
      <el-container>
         <!-- 右侧头部 -->
        <el-header>
          <!-- 面包屑 -->
          <Breadcrumb/>
        </el-header>
        <!-- 右侧主体 -->
        <el-main>
            <router-view />
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>

<script setup>
import {ref} from 'vue'
import NavMenu from './navMenu.vue' //导航  
import Breadcrumb from './breadcrumb.vue'

const title = ref('首页')

</script>