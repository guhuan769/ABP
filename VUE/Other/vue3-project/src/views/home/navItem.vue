<template>
     <!-- 无子级 -->
    <el-menu-item :index="item.url" v-if="!item.child">
        <el-icon><component :is="item.icon"></component></el-icon>
        <span>{{item.title}}</span>
    </el-menu-item>

    <!-- 有子级 -->
    <el-sub-menu :index="item.url" v-else>
        <template #title>
            <el-icon><component :is="item.icon"></component></el-icon>
            <span>{{item.title}}</span>
        </template>
        <NavItem v-for="sub in item.child" :key="sub.url" :item="sub" />
    </el-sub-menu>
</template>

<script setup>
import {ref,reactive,defineProps} from 'vue'

//解构{item}
const {item} = defineProps({
    item:{
        type:Object
    }
})
</script>