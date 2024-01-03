import http from './http'

//登录
export const login = data=>http.post('user/login',data);