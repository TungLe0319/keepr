<template>
  <div class=" card elevation-5 rounded">
    <img :src="keep.img" alt="" title="keep" class="test img-fluid" />
    <div class="card-img-overlay border-none">
      <span>
        <p>{{keep.name}}</p>
      </span>
      <span> <img :src="keep.creator.picture" alt="" class="pImg rounded" height="30">
      </span>
    </div>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { Keep } from "../models/Keep.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  props: {
    keep: { type: Keep, required: true },
  },
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {});

    return {
      editable,
    };
  },
};
</script>

<style lang="scss" scoped>

</style>
