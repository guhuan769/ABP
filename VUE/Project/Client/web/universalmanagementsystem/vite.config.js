import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import path from 'path'
import Components from 'unplugin-vue-components/vite'
import {ElementPlusResolver} from 'unplugin-vue-components/resolvers'

// https://vitejs.dev/config/
export default defineConfig({
  server: {
    open: true,
    port:8088,
  },
  resolver: {
    alias: {
      "@":path.resolve(__dirname,"srs"),
    } // 别名解析
  },
  plugins: [
    vue(),
    Components({
      resolvers:[ElementPlusResolver()]
    })
  ],
})
