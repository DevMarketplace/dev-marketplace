<template>
<h1 @click="hello">hello world</h1>
</template>

<script>
export default {
  methods: {
    // type annotation!
    hello(): void {
      alert('hello world')
    }
  }
}
</script>