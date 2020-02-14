<template>
  <section class="hero is-light is-medium">
    <div class="hero-body">
      <div class="container content">
        <div v-if="isLoading" class="is-size-5 has-text-centered">
            Estamos preparando el componente ..
        </div>
        <template v-else>
          <p class="has-text-centered">
            El tipo de cambio actual para el euro es de
            <b>€{{currency.rate}}</b> por dolar americano.
          </p>
          <div class="columns">
            <div class="column">
              <div class="control">
                <label>Dólares americanos</label>
                <input
                  ref="currency"
                  v-if="hasFocus"
                  @blur="addOrRemoveFocus(false)"
                  v-model.number="from"
                  class="input is-large"
                  type="number"
                  placeholder="USD"
                />
                <span
                  v-else
                  @click="addOrRemoveFocus(true)"
                  class="input is-large"
                >{{from|currency('USD')}}</span>
              </div>
            </div>
            <div class="column">
              <div class="control">
                <label>Euros</label>
                <span class="input is-large">{{to|currency(currency.code)}}</span>
              </div>
            </div>
          </div>

          <button @click="calculate" class="button is-primary is-large is-fullwidth">Calcular</button>
        </template>
      </div>
    </div>
  </section>
</template>

<script src="./exchangerate.js"></script>