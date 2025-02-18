import NextAuth from 'next-auth'
import {PrismaAdapter} from "@auth/prisma-adapter";
import {prisma} from "@/db/prisma";
import CredentialsProvider from "next-auth/providers/credentials";
import {compareSync} from "bcrypt-ts-edge";
import {NextAuthConfig} from "next-auth";

export const config: NextAuthConfig = {
  pages: {
    signIn: '/sign-in',
    error: '/sign-in',
  },
  session: {
    strategy: 'jwt',
    maxAge: 30 * 24 * 60 * 60, // 30 days
  },
  adapter: PrismaAdapter(prisma),
  providers: [
      CredentialsProvider({
        credentials: {
          email: { type: 'email' },
          password: { type: 'password' },
        },
        async authorize(credentials) {
          if (credentials === null || !credentials.email) {
            return null;
          }

          const user = await prisma.user.findFirst({
            where: {
              email: credentials.email,
            }
          });

          if (user?.password && credentials.password && compareSync(<string>credentials.password, user.password)) {
            return {
              id: user.id,
              name: user.name,
              email: user.email,
              role: user.role,
            };
          }

          return null;
        }
      }),
  ],
  callbacks: {
    async session({ session, token, trigger, user }) {
      if (token.sub) {
        session.user.id = token.sub;
      }
      if (trigger === 'update') {
        session.user.name = user.name;
      }

      return session;
    }
  },
};

export const { handlers, auth, signIn, signOut } = NextAuth(config);