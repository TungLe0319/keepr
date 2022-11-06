<template>
  <!-- Modal -->
  <div
    class="modal fade"
    tabindex="-1"
    aria-labelledby="exampleModalLabel"
    aria-hidden="true"
  >
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content  ">
     
     
        <slot>
          
        </slot>
      

        <!-- <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button> -->
        <!-- <button type="button" class="btn btn-primary">Save changes</button> -->
      </div>
    </div>
  </div>
</template>

<script>
import { computed, ref, watchEffect } from 'vue';
import { AppState } from '../AppState';
import { accountService } from '../services/AccountService.js';
import Pop from '../utils/Pop.js';
export default {
  setup() {
    const editable = ref({});
    watchEffect(() => {
    });
    return {
      editable,

      account: computed(() => AppState.account),

     
    };
  },
};
</script>

<style scoped>
.forcedImg {
  border-radius: 4px;
  border: dashed 4px rgb(146, 197, 245);
}
.checkbox {
  width: 30px;
  height: 30px;
}

.Img1 {
  transition: all 0.5s ease;
}
.Img1:hover {
  transform: scale(1.01);
  transition: all 0.4s ease;
  filter: brightness(110%);
}

.checkBoxInput input {
  color: #27132a;
  background-color: #27132a;
  padding: 0 10px;
  font-size: 0.65em;

  border: 1px solid #92c5f5;

  letter-spacing: 0.2em;
  transition: all 1s ease;
}
.inputBox {
  position: relative;
  width: 250px;
}

.inputBox input {
  width: 100%;
  padding: 10px;
  border: 1px solid #92c5f5;
  border-bottom: 3px solid #c4def7;
  border-radius: 5px;
  outline: none;
  color: #fff;
  font-size: 1em;
  background-color: transparent;
  transition: all 1.5s ease;
  box-shadow: 0.25px 0.25px 10px rgba(243, 236, 236, 0.308);
}
.inputBox span {
  position: absolute;
  left: 0;
  bottom: 3em;
  padding: 10px;
  pointer-events: none;
  font-size: 1em;
  text-transform: uppercase;
  color: #f1ecf180;
  transition: all 1s ease;
}
.inputBox input:valid ~ span,
.inputBox input:focus ~ span {
  color: #27132a;
  transform: translateX(10px) translateY(-7px);
  padding: 0 10px;
  font-size: 0.65em;
  border-radius: 4px;
  font-weight: bold;
  background: #92c5f5;
  border-left: 1px solid #92c5f5;
  border-right: 1px solid #92c5f5;
  letter-spacing: 0.2em;
  transition: all 1s ease;
}
.modal {
background: rgb(144,119,160);
background: linear-gradient(90deg, rgba(144,119,160,1) 0%, rgba(250,252,226,0.6695089285714286) 100%);
    backdrop-filter: blur(4px);
}
</style>
