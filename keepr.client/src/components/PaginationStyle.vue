<template>
  <div class="row justify-content-center mt-2">
    <div class="col-md-12 text-center">
      <div class="fixed-bottom justify-content-end d-flex">
        <div class="btn-group dropup">
          <button
            type="button"
            class="btn dropdown-toggle"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            paginate
          </button>
          <ul class="dropdown-menu rounded bg-info bShadow py-0 border-0">
            <li class="d-flex justify-content-center dotHover rounded">
              <button
                @click="togglePaginate(1)"
                class="btn d-flex align-items-center px-0"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#pagination"
                aria-expanded="false"
                aria-controls="collapseExample"
              >
                <img
                  src="https://cdn-icons-png.flaticon.com/512/5558/5558747.png"
                  alt=""
                  height="40"
                  width="40"
                />
                <h6 class="align-items-center d-flex ms-2">buttons</h6>
              </button>
            </li>
            <li class="d-flex justify-content-center dotHover rounded">
              <button
                @click="togglePaginate(0)"
                class="btn d-flex align-items-center px-0"
                type="button"
                data-bs-toggle="collapse"
                data-bs-target="#pagination"
                aria-expanded="false"
                aria-controls="collapseExample"
              >
                <img
                  src="https://cdn-icons-png.flaticon.com/512/3646/3646818.png"
                  alt=""
                  height="40"
                  width="40"
                />
                <h6 class="align-items-center d-flex ms-2">scroll</h6>
              </button>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div class="collapse" id="pagination">
      <div class="">
        <nav aria-label="Page navigation example">
          <ul class="pagination d-flex justify-content-center">
            <li class="page-item">
              <button class="btn page-link text-dark" @click="paginate('left')">
                <i class="mdi mdi-arrow-left-box fs-2"></i>
              </button>
            </li>
            <li class="page-item">
              <button
                class="btn page-link text-dark"
                @click="paginate('right')"
              >
                <i class="mdi mdi-arrow-right-box fs-2"></i>
              </button>
            </li>
          </ul>
        </nav>
      </div>
    </div>
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
  props: {},
  setup(props) {
    const editable = ref({});

    onMounted(() => {});
    watchEffect(() => {});

    return {
      editable,
      async paginate(direction) {
        try {
          if (direction == "left") {
            await keepsService.paginate("prev");
          } else {
            await keepsService.paginate("next");
          }
        } catch (error) {
          Pop.error(error, "[paginate]");
        }
      },
      togglePaginate(style) {
        if (style == 0) {
          AppState.paginationOn = false;
        } else AppState.paginationOn = true;
      },
    };
  },
};
</script>

<style lang="scss" scoped></style>
