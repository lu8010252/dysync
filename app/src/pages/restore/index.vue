<template>
  <div class="restore-wrap">
    <iframe class="restore-frame" src="/restore.html" title="恢复下载"></iframe>
  </div>
</template>

<script setup lang="ts"></script>

<style scoped>
.restore-wrap {
  width: 100%;
  height: calc(100vh - 120px);
  min-height: 480px;
  border-radius: 8px;
  overflow: hidden;
}
.restore-frame {
  width: 100%;
  height: 100%;
  border: 0;
  background: #141414;
}
</style>
