<template>
  <div class="d-flex justify-content-end align-items-center mt-2">
    <div class="collapse collapse-horizontal" id="collapseWidthExample">
      <div class="card bg-dark" style="width: 300px">
        <form @keyup="searchByName()" class="">
          <div class="d-flex align-items-center">
            <input
              v-model="editable"
              type="text"
              class="form-control phtext"
              placeholder="Search for recipes . . ."
              aria-label="Search"
              aria-describedby="button-addon2"
            />
          </div>
        </form>
      </div>
    </div>

    <button
      class="btn"
      type="submit"
      id="button-addon2"
      data-bs-toggle="collapse"
      data-bs-target="#collapseWidthExample"
  title="Search"
    >
      <img
        src="https://cdn-icons-png.flaticon.com/512/751/751463.png"
        alt=""
        width="50"
        height="50"
      />
    </button>
  </div>
</template>

<script>
import { computed } from "@vue/reactivity";
import { onMounted, ref, watchEffect } from "vue";
import { AppState } from "../AppState.js";
import { keepsService } from "../services/KeepsService.js";
import { logger } from "../utils/Logger.js";
import Pop from "../utils/Pop.js";

export default {
  setup() {
    const editable = ref("");
    onMounted(() => {
      // hideOnScrollTest();
    });
    watchEffect(() => {});

    return {
      editable,

      async searchByName() {
        try {
          await keepsService.getKeepsByQuery()
        } catch (error) {
          logger.error("[searchByQuery]", error);
          console.error("dfd", error);
        }
      },
    };
  },
};
</script>

<style lang="scss" scoped>
.phtext {
  font-size: 12px;
}
</style>
