const apiBaseUrl = import.meta.env.VITE_API_BASE_URL ?? 'http://localhost:5080'

async function readError(response) {
  const body = await response.json().catch(() => null)

  if (body?.errors) {
    return Object.values(body.errors).flat().join(' ')
  }

  return body?.detail ?? body?.title ?? `API-anropet misslyckades (${response.status}).`
}

async function request(path, options) {
  const response = await fetch(`${apiBaseUrl}${path}`, options)

  if (!response.ok) {
    throw new Error(await readError(response))
  }

  return response.status === 204 ? null : response.json()
}

export function getProducts() {
  return request('/api/products')
}

export function getProduct(id) {
  return request(`/api/products/${id}`)
}

export function createProduct(product) {
  return request('/api/products', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(product),
  })
}

export function deleteProduct(id) {
  return request(`/api/products/${id}`, { method: 'DELETE' })
}
