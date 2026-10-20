<template>
  <div class="restore-page">
    <a-card title="恢复下载(不再下载黑名单)" :bordered="false">
      <p class="tip">
        在 dysync 里「彻底删除」过的视频会被记入黑名单，之后同步会一直跳过。勾选后点「恢复选中」，或点「全部恢复」，下次同步会重新下载。
      </p>
      <a-space style="margin-bottom: 12px" wrap>
        <a-button @click="toggleAll">全选/取消</a-button>
        <a-button type="primary" :disabled="!selected.length" :loading="working" @click="restore(selected)">
          恢复选中{{ selected.length ? `（${selected.length}）` : '' }}
        </a-button>
        <a-popconfirm title="确定清空整个黑名单吗？" @confirm="restore([])">
          <a-button danger :disabled="!items.length" :loading="working">全部恢复</a-button>
        </a-popconfirm>
        <a-button @click="load" :loading="loading">刷新</a-button>
      </a-space>
      <a-spin :spinning="loading">
        <a-empty v-if="!items.length && !loading" description="黑名单是空的" />
        <a-checkbox-group v-else v-model:value="selected" style="width: 100%">
          <div v-for="x in items" :key="x.id" class="row">
            <a-checkbox :value="x.id">
              <span class="t">{{ x.videoTitle || x.viedoId }}</span>
              <small>{{ x.videoSavePath }} · {{ fmt(x.deleteTime) }}</small>
            </a-checkbox>
          </div>
        </a-checkbox-group>
      </a-spin>
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { onMounted, ref } from 'vue';
import { message } from 'ant-design-vue';
import { useApiStore } from '@/store';

const apiStore = useApiStore();
const items = ref<any[]>([]);
const selected = ref<string[]>([]);
const loading = ref(false);
const working = ref(false);

const fmt = (t?: string) => (t || '').replace('T', ' ').slice(0, 19);

const load = async () => {
  loading.value = true;
  try {
    const res: any = await apiStore.GetDeleteViedos();
    items.value = res?.data || [];
    selected.value = [];
  } catch (e: any) {
    message.error(e?.message || '加载失败');
  } finally {
    loading.value = false;
  }
};

const toggleAll = () => {
  selected.value = selected.value.length === items.value.length ? [] : items.value.map(x => x.id);
};

const restore = async (ids: string[]) => {
  working.value = true;
  try {
    const res: any = await apiStore.RestoreDeleteViedos(ids);
    if (res?.code !== 0) {
      message.error(res?.message || '恢复失败');
      return;
    }
    message.success(res?.data || res?.message || '已恢复，下次同步会重新下载');
    await load();
  } catch (e: any) {
    message.error(e?.message || '恢复失败');
  } finally {
    working.value = false;
  }
};

onMounted(load);
</script>

<style scoped>
.restore-page { padding: 16px; max-width: 960px; }
.tip { color: #888; line-height: 1.7; }
.row { padding: 8px 0; border-bottom: 1px solid rgba(128, 128, 128, 0.2); }
.t { word-break: break-all; }
small { display: block; color: #888; margin: 2px 0 0 24px; }
</style>
