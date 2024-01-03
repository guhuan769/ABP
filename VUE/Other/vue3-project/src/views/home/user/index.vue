<template>
   <div>
      <router-view/>
   </div>
</template>

<script setup>
import {ref,reactive} from 'vue'

const obj = reactive({   //响应式数据
   title:'用户管理8888',
   count:1
})

const title = ref('123456');  //响应式数据   const title = reactive({value:'123456'})  title.value

const abc = 'abc';  //非响应式数据

function action(){

}
const action2 = ()=>{

}

</script>

// <script >
// import {defineComponent, ref,reactive} from 'vue'
// export default defineComponent({
//    setup(){
//       //创建响应式数据
//       const state = reactive({
//          title:'用户管理8888',
//          count:1
//       })
//        return state;
//    }
// })
// </script>