<template>
   <div id="module">
    <el-container>
         <!-- 左侧导航 -->
         <NavMenu />
      <el-container>
         <!-- 右侧头部 -->
        <el-header>Header</el-header>
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

const title = ref('首页')

</script>