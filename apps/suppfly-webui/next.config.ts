import type { NextConfig } from "next";
import path from "path";

/** @type {import('next').NextConfig}*/
const nextConfig: NextConfig = {
  turbopack: {
    root: path.resolve(__dirname, '../../'),
  },
  experimental: {

  }
};


export default nextConfig;
