/*
 * @Author: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @Date: 2024-01-02 14:48:01
 * @LastEditors: error: error: git config user.name & please set dead value or install git && error: git config user.email & please set dead value or install git & please set dead value or install git
 * @LastEditTime: 2024-01-02 17:46:16
 * @FilePath: \universalmanagementsystem\src\api\index.js
 * @Description: 这是默认设置,请设置`customMade`, 打开koroFileHeader查看配置 进行设置: https://github.com/OBKoro1/koro1FileHeader/wiki/%E9%85%8D%E7%BD%AE
 */
import axios from 'axios'
import { ElMessage } from 'element-plus'

//访问地址
axios.defaults.baseURL="http://117.174.127.220:5059/api/";

axios.interceptors.request.use(config=>{
    //设置请求头，比如携带token
    //config.headers[]
    return config
})

axios.interceptors.response.use(res=>{
    //错误码的处理
    let {code,msg} = res.data;
    if(code !==200){
        ElMessage({
            message: msg ||'Warning, this is a warning message.',
            type: 'warning',
            duration:1000
        })
    }
    return res
},err=>{
    //404 500
    return Promise.reject(err)
})

export default axios