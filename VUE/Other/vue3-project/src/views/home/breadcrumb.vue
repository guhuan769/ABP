<!--
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2023-12-29 16:41:40
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2023-12-30 17:48:27
 * @FilePath: \vue3-project\src\views\home\index.vue
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
-->
<template>
  <div class="breadcrumb">
    <el-breadcrumb separator="/">
    <el-breadcrumb-item v-for="v in lists" :key="v.path" >
      <router-link :to="v.path">{{v.meta.title}}</router-link>
    </el-breadcrumb-item>
  </el-breadcrumb>
  </div>
</template>

<script setup>
import {onMounted, ref,watch} from 'vue'
// useRouter 实例对象 useRoute 路由信息
import {useRouter,useRoute} from 'vue-router'

const route = useRoute(); //实例化 路由信息
const lists = ref([]); // 面包屑得数据

//添加一个监听
watch(route,(newValue,oldValue)=>{
  getBreadcrumb(newValue.matched);
});

onMounted(()=>{ // 挂载完成 onMounte只有第一次初始化得时候执行Onmounte
  console.log(12345);
  getBreadcrumb(route.matched);
})

function getBreadcrumb(matched){
  lists.value = matched; // 路由信息
}



</script>