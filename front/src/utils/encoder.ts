export function toFormUrlEncoded(params: Partial<{ [p: string]: any }>) {
  const form = Object.entries(params).reduce((string, pair) => {
    const [key, value] = pair;
    const enodedKey = encodeURIComponent(key);
    const enodedValue = encodeURIComponent(value);

    const keyValue = `${enodedKey}=${enodedValue}`;
    return string ? `${string}&${keyValue}` : keyValue;
  }, '');

  return form;
}
