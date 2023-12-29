<template>
   <el-aside width="200px" class="aside"> 
        <el-menu
            default-active="2"
            class="el-menu-vertical-demo" router
        >
            <el-menu-item index="/index">
                <el-icon><icon-menu /></el-icon>
                <span>首页</span>
            </el-menu-item>

            <el-menu-item index="/user">
                <el-icon><icon-menu /></el-icon>
                <span>用户管理</span>
            </el-menu-item>

            <el-menu-item index="/device">
                <el-icon><icon-menu /></el-icon>
                <span>设备监控</span>
            </el-menu-item>
        </el-menu>
    </el-aside>
</template>

<script setup>
import {ref} from 'vue'
import {
  Document,
  Menu as IconMenu,
  Location,
  Setting,
} from '@element-plus/icons-vue'

const title = ref('首页')

</script>