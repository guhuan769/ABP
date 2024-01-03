<script setup>
import { ref, reactive } from "vue";
import { Connection, MessageBox } from "@element-plus/icons-vue";

const ruleFormRef = ref();
const ruleForm = reactive({
  account: "admin",
  password: "",
});

const rules = reactive({
  account: [
    { required: true, message: "Please input Activity name", trigger: "blur" },
    { min: 3, max: 18, message: "Length should be 3 to 18", trigger: "blur" },
  ],
  password: [
    { required: true, message: "Please input Activity name", trigger: "blur" },
    { min: 3, max: 18, message: "Length should be 3 to 18", trigger: "blur" },
  ],
});
//登录
function submitForm(formEl) {
  formEl.validate(async (valid) => {
    if (valid) {
      //校验
      //加密处理
      ruleForm.password = md5(ruleform.password);
      let res = await login(ruleForm);
      let {
        code,
        data: { token },
      } = res.data;

      if (code === 200) {
        // 存
      }
    } else {
    }
  });
}
</script>

<template>
  <div class="login">
    <div class="login-content">
      <div class="login-item login-box">
        <i
          ><el-icon><Connection /></el-icon
        ></i>
        <p>软件平台</p>
      </div>
      <div class="login-item login-box">
        <i
          ><el-icon><MessageBox /></el-icon
        ></i>
        <p>软件平台</p>
      </div>
      <!-- 登录 -->
      <div class="login-item login-form">
        <p class="login-title">低代码平台管理系统</p>
        <el-form
          ref="ruleFormRef"
          :model="ruleForm"
          :rules="rules"
          label-width="120px"
          class="demo-ruleForm"
          status-icon
        >
          <el-form-item label="用户名" prop="account">
            <el-input v-model="ruleForm.account" />
          </el-form-item>

          <el-form-item label="密码" prop="password">
            <el-input type="password" v-model="ruleForm.password" />
          </el-form-item>

          <el-form-item>
            <el-button type="success" @click="submitForm(ruleFormRef)">
              登录
            </el-button>
          </el-form-item>
        </el-form>
      </div>
      <div class="login-item login-box">
        <i
          ><el-icon><Connection /></el-icon
        ></i>
        <p>软件平台</p>
      </div>
      <div class="login-item login-box">
        <i
          ><el-icon><Connection /></el-icon
        ></i>
        <p>软件平台</p>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
